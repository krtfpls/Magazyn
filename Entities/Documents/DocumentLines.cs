// namespace Entities.Documents
// {
//     public class DocumentLines
//     {
//         public List<DocumentLine> documentLines {get; private set;} = new List<DocumentLine>();

//         public virtual void AddLine (Product product, int quantity){

//             DocumentLine? line = documentLines
//                             .Where(p => p.Product.Name == product.Name)
//                             .FirstOrDefault();

//                 if (line == null){
//                     documentLines.Add(new DocumentLine{
//                         Product= product,
//                         Quantity = quantity
//                     });
//                 }
//                 else{
//                     line.Quantity+=quantity;
//                 }
//         }

//         public virtual void RemoveLine (Product product) =>
//             documentLines.RemoveAll(l => l.Product.Id == product.Id);
        
//         public decimal ComputeTotalValue() => 
//             documentLines.Sum(e => e.Product.PriceNetto * e.Quantity);

//         public virtual void Clear() => documentLines.Clear();
//     }
// }