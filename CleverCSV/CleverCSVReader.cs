using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace CleverCSVReader
{
    public class CleverCSVReader : CsvReader
    {
        public CleverCSVReader(IParser parser) : base(parser)
        {
        }

        public CleverCSVReader(TextReader reader, CultureInfo culture, bool leaveOpen = false) : base(reader, culture, leaveOpen)
        {
        }

        public CleverCSVReader(TextReader reader, IReaderConfiguration configuration, bool leaveOpen = false) : base(reader, configuration, leaveOpen)
        {
        }
    }
}
