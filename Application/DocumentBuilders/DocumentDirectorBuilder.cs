using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Entities.Documents;

namespace Application.DocumentBuilders
{
    public class DocumentDirectorBuilder
    {
        private readonly DocumentBuilder _builder;
        public DocumentDirectorBuilder(DocumentBuilder builder)
        {
            this._builder = builder;
        }

        public void AddNumber(int number)
        {
            _builder.SetNumber(number);
        }

        public void AddType (int type){
            _builder.SetType(type);
        }

        public void AddDate(DateTime date)
        {
            _builder.SetDate(date);
        }
        public void AddDocumentLine(DocumentLine line)
        {
            _builder.AddProductLine(line);
        }

        public void AddCustomer(int customerId){
            _builder.SetCustomer(customerId);
        }

        public Document Build()
        {
            return _builder.Build();
        }
    }
}