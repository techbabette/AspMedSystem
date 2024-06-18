using AspMedSystem.Application.Exceptions;
using AspMedSystem.Application.UseCases.Queries.Reports;
using AspMedSystem.DataAccess;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases.Queries.Reports
{
    public class EfReportSearchPDFQuery : EfUseCase, IReportSearchPDFQuery
    {
        private EfReportSearchPDFQuery()
        {
        }

        public EfReportSearchPDFQuery(MedSystemContext context) : base(context)
        {
        }

        public string Name => "Show report as pdf";

        public byte[] Execute(int search)
        {
            var report = Context.Reports.Where(rt => rt.Id == search)
                .Include(rt => rt.UserTreatments)
                .ThenInclude(ut => ut.Treatment)
                .Include(rt => rt.Examination)
                .ThenInclude(exam => exam.Examinee)
                .Include(rt => rt.Examination)
                .ThenInclude(exam => exam.ExaminationTerm)
                .ThenInclude(term => term.Examiner)
                
                .FirstOrDefault();

            if(report == null)
            {
                throw new EntityNotFoundException("Report", search);
            }

            using MemoryStream memoryStream = new MemoryStream();
             
            using PdfWriter writer = new PdfWriter(memoryStream);
            using PdfDocument reportPDF = new PdfDocument(writer);

            using Document document = new Document(reportPDF);

            document.Add(new Paragraph($"PDF generated on {DateTime.Now.ToLongDateString()}"));

            document.Add(new Paragraph($"Examiner: {report.Examination.ExaminationTerm.Examiner.FirstName} {report.Examination.ExaminationTerm.Examiner.LastName}"));

            document.Add(new Paragraph($"Examinee: {report.Examination.Examinee.FirstName} {report.Examination.Examinee.LastName}"));

            document.Add(new Paragraph($"Examination time: {report.Examination.ExaminationTerm.Date}"));

            document.Add(new Paragraph($"Findings: {report.Text}"));

            if (report.UserTreatments.Any())
            {
                document.Add(new Paragraph("Prescribed treatments:"));
                foreach(var tr in report.UserTreatments)
                {
                    if(tr.EndDate < tr.StartDate)
                    {
                        document.Add(new Paragraph($"{tr.Treatment.Name} cancelled early \n {tr.Note}"));
                        continue;
                    }
                    document.Add(new Paragraph($"{tr.Treatment.Name}  {tr.StartDate} - {tr.EndDate} \n {tr.Note}"));
                }
            }

            document.Close();

            byte[] pdfBytes = memoryStream.ToArray();

            return pdfBytes;
        }
    }
}
