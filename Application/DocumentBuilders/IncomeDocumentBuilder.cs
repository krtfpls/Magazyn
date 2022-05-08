using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Entities.Documents;

namespace Application.DocumentBuilders
{
    public abstract class IncomeDocumentBuilder : DocumentBuilder
    {
        public override void AddProductLine(DocumentLine documentLine)
        {
            var line = documentLine;
            line.Product.Quantity += line.Quantity;
            _lines.Add(line);
        }

    }
}