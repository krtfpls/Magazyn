using Application.Documents.DocumentHelpers;
using Entities;
using Entities.Documents;

namespace Application.Documents.DocumentBuilder
{
    public class GoodsDispatchNoteBuilder: IDocumentBuilder
    {
        private Document document;
        private List<DocumentLine> documentLines;
        private readonly DocumentTypesEnum type;

        public GoodsDispatchNoteBuilder()
        {
            document = new Document();
            document.Id= new Guid();
            type= DocumentTypesEnum.WZ;
            documentLines = new List<DocumentLine>();
        }

        public void SetCustomer(int customerId)
        {
            document.CustomerId= customerId;
        }

        public void SetDate(DateOnly date)
        {
            document.Date= date;
        }

        public void AddLine(Product product, int qty)
        {
            DocumentLine? line =StandardDispatchDocumentLineStrategy.handleLine(product, qty);
            if (line != null)
                documentLines.Add(line);
        }

        public void SetNumber(string number)
        {
            document.Number= number;
        }

        public void SetType(int type)
        {
            document.TypeId= type;
        }

        public void SetUser(String userId)
        {
            document.UserId= userId;
        }

        string IDocumentBuilder.GetType()
        {
            return type.ToString();
        }

         public Document? Build()
        {
             if (document.UserId == null || documentLines.Count < 1 || document.CustomerId == 0 ||
                    document.Number== null ||document.TypeId == 0)
                {
                Console.WriteLine("Not all Document parts are set!");
                return null;
                    }
            if (document.Date == null)
                document.Date= DateOnly.FromDateTime(DateTime.Now);

            document.DocumentLines= documentLines;

            return document;
        }
    }
}