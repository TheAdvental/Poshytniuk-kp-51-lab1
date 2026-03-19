using System.Diagnostics;

namespace Task
{
    public class Sorter
    {
        private List<Record> _collection;
        private SortStatistics _stats;

        public void InitCollection()
        {
            _collection = new List<Record>();
            _stats = new SortStatistics();
        }

        public void AddRecord(Record record)
        {
            if (record.PublicationId < 0)
            {
                Console.WriteLine("Publication ID is negative.");
                return;
            }
            if (record.CitationCount < 0)
            {
                Console.WriteLine("Citation count is negative.");
                return;
            }
            foreach (Record r in _collection)
            {
                if (r.PublicationId == record.PublicationId)
                {
                    Console.WriteLine("Publication with this ID is already exist.");
                    return;
                }
            }

            _collection.Add(record);
        }

        public void RemoveRecord(int PublicationId)
        {
            for (int i = 0; i < _collection.Count; i++)
            {
                if (_collection[i].PublicationId == PublicationId)
                {
                    _collection.RemoveAt(i);
                    Console.WriteLine("Record removed.");
                    return;
                }
            }
            Console.WriteLine("Record isn`t in collection.");
        }

        public void PrintCollection()
        {
            Console.WriteLine("ID | Author | Title | Citations\n");
            foreach (Record r in _collection)
            {
                Console.WriteLine($"{r.PublicationId} | {r.AuthorSurname} | {r.Title} | {r.CitationCount}");
            }
        }

        public void GenerateControlData()
        {
            AddRecord(new Record(101, "Smith", "Quantum Computing", 150));
            AddRecord(new Record(102, "Adams", "AI Ethics", 85));
            AddRecord(new Record(103, "Brown", "Data Science", 85));
            AddRecord(new Record(104, "Aaron", "Cyber Security", 200));
            AddRecord(new Record(105, "Garcia", "Web Frameworks", 12));
            AddRecord(new Record(106, "Miller", "Cloud Systems", 50));
            AddRecord(new Record(107, "Adams", "Machine Learning", 150));
            AddRecord(new Record(108, "Wilson", "Network Protocols", 30));
            AddRecord(new Record(109, "Taylor", "Game Theory", 85));
            AddRecord(new Record(110, "Anderson", "Mobile Apps", 5));
            AddRecord(new Record(111, "Ray", "Blockchain", 150));
            AddRecord(new Record(112, "Evans", "UX Design", 75));
        }

        public void SortCollection()
        {
            _stats = new SortStatistics();
            Stopwatch timer = Stopwatch.StartNew();

            _collection = MergeSort(_collection);

            timer.Stop();
            _stats.ExecTime = timer.Elapsed;
        }

        private List<Record> MergeSort(List<Record> _collection)
        {
            _stats.RecursiveCalls++;

            if (_collection.Count <= 1)
            {
                return _collection;
            }

            int mid = _collection.Count / 2;
            List<Record> left = new List<Record>();
            List<Record> right = new List<Record>();

            for (int i = 0; i < mid; i++)
            {
                left.Add(_collection[i]);
                _stats.Copies++;
            }
            for (int i = mid; i < _collection.Count; i++)
            {
                right.Add(_collection[i]);
                _stats.Copies++;
            }

            left = MergeSort(left);
            right = MergeSort(right);

            return Merge(left, right);
        }

        private bool IsBetter(Record left, Record right)
        {
            if (left.CitationCount > right.CitationCount)
            {
                return true;
            }
            if (left.CitationCount < right.CitationCount)
            {
                return false;
            }

            return string.Compare(left.AuthorSurname, right.AuthorSurname) < 0;
        }

        private List<Record> Merge(List<Record> left, List<Record> right)
        {
            List<Record> result = new List<Record>();
            int i = 0;
            int j = 0;

            while (i < left.Count && j < right.Count)
            {
                _stats.Comparisons++;
                if (IsBetter(left[i], right[j]))
                {
                    result.Add(left[i]);
                    i++;
                }
                else
                {
                    result.Add(right[j]);
                    j++;
                }
                _stats.Copies++;
            }

            while (i < left.Count)
            {
                result.Add(left[i]);
                i++;
                _stats.Copies++;
            }
            while (j < right.Count)
            {
                result.Add(right[j]);
                j++;
                _stats.Copies++;
            }

            PrintIntermediateSteps(result);

            return result;
        }

        public void PrintIntermediateSteps(List<Record> currMergedList)
        {
            Console.Write("Крок злиття: [ ");
            foreach (Record r in currMergedList)
            {
                Console.Write($"{r.AuthorSurname}({r.CitationCount}) ");
            }
            Console.WriteLine("]");
        }

        public void PrintStatistics()
        {
            Console.WriteLine("---Статистика---");
            Console.WriteLine($"Час виконання: {_stats.ExecTime.TotalMilliseconds} мс");
            Console.WriteLine($"К-сть порівнянь: {_stats.Comparisons}");
            Console.WriteLine($"К-сть копіювань: {_stats.Copies}");
            Console.WriteLine($"К-сть рекурсивних викликів: {_stats.RecursiveCalls}");
            Console.WriteLine($"---------------");
        }

        public void PrintTop10()
        {
            Console.WriteLine("---Топ-10 наукових публікацій---");
            Console.WriteLine("ID | Author | Title | Citations");

            int limit = _collection.Count;

            if (_collection.Count < 10)
            {
                limit = _collection.Count;
            }
            else
            {
                limit = 10;
            }

            for (int i = 0; i < limit; i++)
            {
                Record r = _collection[i];
                Console.WriteLine($"{r.PublicationId} | {r.AuthorSurname} | {r.Title} | {r.CitationCount}");
            }
        }
    }
}
