//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Малахов.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public int ID { get; set; }
        public int IDClient { get; set; }
        public int IDService { get; set; }
        public Nullable<int> IDManager { get; set; }
        public int Count { get; set; }
        public int DurationInSecond { get; set; }
        public decimal Price { get; set; }
        public System.DateTime Date { get; set; }
        public bool Status { get; set; }
        public byte[] Music { get; set; }
        public string Content { get; set; }
        public string StatusString => Status ? "Одобрено" : "Не одобрено";
        public string Sum => $"Стоимость с включающими услугами: {Price} руб.";

        public virtual Client Client { get; set; }
        public virtual Manager Manager { get; set; }
        public virtual Service Service { get; set; }
    }
}
