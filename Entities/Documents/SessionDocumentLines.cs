// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text.Json.Serialization;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Http;
// using Microsoft.Extensions.DependencyInjection;

// namespace Entities.Documents
// {
//     public class SessionDocumentLines: DocumentLines
//     {
//         public static DocumentLines GetProductLines(IServiceProvider services){

//             ISession? session = services.GetRequiredService<HttpContextAccessor>()?
//                         .HttpContext.Session;

//             SessionDocumentLines workFlow = session?.GetJson<SessionDocumentLines>("DocumentLines")
//                         ?? new SessionDocumentLines();

//             workFlow.Session = session; 

//             return workFlow;
//         }    

//         [JsonIgnore]
//         public ISession? Session {get; set;}
//         public override void AddLine(Product product, int quantity){
//             base.AddLine(product, quantity);
//             Session?.SetJson("DocumentLines", this);
//         }

//         public override void RemoveLine(Product product)
//         {
//             base.RemoveLine(product);
//             Session?.SetJson("DocumentLines", this);
            
//             }

//         public override void Clear(){
//             base.Clear();
//             Session?.Remove("DocumentLines");
//         }
//     }
// }