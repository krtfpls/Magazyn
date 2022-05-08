using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Entities.Documents;

namespace Application.DocumentBuilders
{
    public abstract class DocumentBuilder
    {
        public string? TypeName {get; protected set;}
        protected DateTime _date;
        protected int _number;
        protected int _typeId;
        protected int _customerId;
        protected List<DocumentLine> _lines = new List<DocumentLine>();

        public virtual void SetType(int type){
            _typeId = type;
        }
        public virtual void SetNumber(int number)
        {
            _number = number;
        }
        public virtual void SetDate(DateTime date)
        {
            _date = date;
        }

        public virtual void SetCustomer(int customerId){
            _customerId= customerId;
        }
        public abstract void AddProductLine(DocumentLine productLine);
        public virtual Document Build()
        {

            if (_lines == null)
                throw new Exception("Docuent has no lines!");
            if (_number == 0)
                throw new Exception("Document has no number");
            if (_typeId == 0)
                throw new Exception("Document has no type!");
             if (_date == DateTime.MinValue)
                _date = DateTime.Now;
            if (_customerId == 0)
                throw new Exception("Document has no customer");
            Document document = new Document();
            document.CustomerId=_customerId;
            document.Date = this._date;
            document.Number = this._number.ToString();
            document.TypeId = this._typeId;
            document.DocumentLines = this._lines;

            return document;
        }
    }
}