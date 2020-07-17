using System.Collections;
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
