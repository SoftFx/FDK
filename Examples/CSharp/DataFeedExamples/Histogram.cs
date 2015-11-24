namespace DataFeedExamples
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class Histogram
    {
        public Histogram(int maxSize)
        {
            this.m_distribution = new List<double>(maxSize);
        }

        public void Add(int key, double weight)
        {
            if (key < 0)
            {
                throw new ArgumentException("key should be positive", "key");
            }

            if (weight < 0)
            {
                throw new ArgumentException("weight should be positive", "weight");
            }

            if (key >= this.m_distribution.Capacity)
            {
                return;
            }

            while (key >= this.m_distribution.Count)
            {
                this.m_distribution.Add(0);
            }

            this.m_distribution[key] += weight;
        }

        public override string ToString()
        {
            return this.ToString(int.MaxValue);
        }

        public string ToString(int maximumLineSize)
        {
            var builder = new StringBuilder();
            builder.Append("{");

            var currentLineLength = 1;
            var count = this.m_distribution.Count - 1;

            for (var position = 0; position < count; ++position)
            {
                currentLineLength = this.FormatNextLine(position, currentLineLength, maximumLineSize, builder);
            }

            if (count > 0)
            {
                var st = this.FormatValue(count);
                if (currentLineLength + st.Length > maximumLineSize)
                {
                    builder.Append("\n");
                }
                builder.Append(st);
            }
            builder.Append("};");
            var result = builder.ToString();

            return result;
        }

        int FormatNextLine(int position, int currentLineLength, int maximumLineSize, StringBuilder builder)
        {
            var st = this.FormatValue(position);
            var requiredValueLength = st.Length + ", ".Length;

            if (currentLineLength + requiredValueLength > maximumLineSize)
            {
                builder.Append("\n");
                currentLineLength = 0;
            }

            builder.Append(st)
                   .Append(", ");

            currentLineLength += requiredValueLength;

            return currentLineLength;
        }

        string FormatValue(int position)
        {
            var value = this.m_distribution[position];
            var result = value.ToString();
            result = result.Replace("E", " 10^");
            return result;
        }

        readonly List<double> m_distribution;
    }
}
