//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CasasRed_Nuevo3_.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Contaduria
    {
        public int Id { get; set; }
        public Nullable<decimal> Cnt_Presupuesto_gestion { get; set; }
        public Nullable<decimal> Cnt_Presupuesto_corretaje { get; set; }
        public Nullable<decimal> Cnt_Presupuesto_habilitacion { get; set; }
        public Nullable<decimal> Cnt_Mensualidad { get; set; }
        public Nullable<decimal> Cnt_Vigilancia { get; set; }
    
        public virtual Corretaje Corretaje { get; set; }
        public virtual Gestion Gestion { get; set; }
    }
}