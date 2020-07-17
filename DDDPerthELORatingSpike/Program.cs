using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace DDDPerthELORatingSpike
{
    class Program
    {
        static void Main(string[] args)
        {
            var (submitters, submissions, votes) = GetVotingData();

            var ratings = submissions.Select(s => s.SubmissionId).ToDictionary(x => x, x => (double)50);

            foreach (var vote in votes.OrderByDescending(v => v.Timestamp))
            {
                for (var pair = 0; pair < vote.DecodedSessionIds.Length - 1; pair++)
                {
                    var winner = vote.DecodedSessionIds[pair];
                    var loser = vote.DecodedSessionIds[pair + 1];
                    var newRatings = new Rating(ratings[winner], ratings[loser], Rating.WIN, Rating.LOSE).GetNewRatings();
                    ratings[winner] = newRatings[0];
                    ratings[loser] = newRatings[1];
                }

                foreach (var submission in submissions.Where(s => !vote.DecodedSessionIds.Contains(s.SubmissionId)))
                {
                    var notVotedSession = submission.SubmissionId;
                    foreach (var votedSession in vote.DecodedSessionIds)
                    {
                        var newRatings = new Rating(ratings[votedSession], ratings[notVotedSession], Rating.WIN, Rating.LOSE).GetNewRatings();
                        ratings[votedSession] = newRatings[0];
                        ratings[notVotedSession] = newRatings[1];
                    }
                }
            }

            var rank = 1;
            File.AppendAllText("out.csv", "Rank,Rating,TotalVotes,SessionId,SessionTitle,Presenters\r\n");
            foreach (var rating in ratings.OrderByDescending(r => r.Value))
            {
                var session = submissions.Single(s => s.SubmissionId == rating.Key).DecodedSession;
                var presenters = submitters.Where(s => session.PresenterIds.Select(id => id.ToString()).Contains(s.SubmitterId))
                    .Select(s => s.DecodedPresenter).ToArray();
                var line = $"{rank},{rating.Value},{GetTotalVotes(votes, session.Id.ToString())},\"{session.Id}\",\"{session.Title.Replace("\"", "\"\"")}\",\"{string.Join("; ", presenters.Select(p => p.Name.Replace("\"", "\"\"")))}\"\r\n";
                Console.WriteLine(line);
                File.AppendAllText("out.csv", line);

                rank++;
            }
        }

        private static int GetTotalVotes(IList<Vote> votes, string sessionId)
        {
            return votes.Count(v => v.DecodedSessionIds.Contains(sessionId));
        }

        private static (IList<Submitter> submitters, IList<Submission> submissions, IList<Vote> votes) GetVotingData()
        {
            IList<Submitter> submitters;
            using (var reader = new StreamReader("Submitters.typed.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                submitters = csv.GetRecords<Submitter>().ToList();
            }

            IList<Submission> submissions;
            using (var reader = new StreamReader("Submissions.typed.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                submissions = csv.GetRecords<Submission>().ToList();
            }

            IList<Vote> votes;
            using (var reader = new StreamReader("Votes.typed.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                votes = csv.GetRecords<Vote>().ToList();
            }

            return (submitters, submissions, votes);
        }
    }
}
