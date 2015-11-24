using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using EncodingTools;

namespace EncodingTest
{
    public partial class EncodingTestForm : Form
    {
        private readonly Encoding m_Encoding;
        private readonly string m_TestText;

        public EncodingTestForm(Encoding enc, string testText)
        {
            InitializeComponent();
            m_Encoding = enc;
            m_TestText = testText;
            DoTest();
        }

        private void DoTest()
        {
            if ((m_TestText == null) || (m_TestText.Length == 0))
                return;
            using (var ms = new MemoryStream())
            {
                var encoded = m_Encoding.GetBytes(m_TestText);
                // preamble?
                var preamble = m_Encoding.GetPreamble();

                // Make sure a preamble was returned 
                // and is large enough to containa BOM.
                if (preamble.Length >= 2)
                {
                    ms.Write(preamble, 0, preamble.Length);
                }

                ms.Write(encoded, 0, encoded.Length);

                ms.Position = 0;
                // read it using standard text reader
                var tr = new StreamReader(ms, true);


                streamReader.Text = tr.ReadToEnd();
                label1.Text = String.Format("StreamReader: {0} / {1}", tr.CurrentEncoding.EncodingName,
                    tr.CurrentEncoding.BodyName);

                // now the improved test
                ms.Position = 0;
                Encoding targetEncoding;
                var rawData = ms.ToArray();
                try
                {
                    targetEncoding = EncodingDetection.DetectInputCodepage(rawData);
                }
                catch (COMException)
                {
                    targetEncoding = Encoding.Default;
                }
                detected.Text = targetEncoding.GetString(rawData);
                label2.Text = String.Format("EncodingDetection.DetectInputCodepage: {0} / {1}", targetEncoding.EncodingName,
                    targetEncoding.BodyName);
            }
        }
    }
}