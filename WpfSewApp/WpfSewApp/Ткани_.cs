//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfSewApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ткани_
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ткани_()
        {
            this.ИзмерениеНоменклатуры = new HashSet<ИзмерениеНоменклатуры>();
            this.СкладТкани = new HashSet<СкладТкани>();
            this.ТканиИзделия = new HashSet<ТканиИзделия>();
        }
    
        public string Артикул { get; set; }
        public string Название { get; set; }
        public string Цвет { get; set; }
        public string Рисунок { get; set; }
        public string Состав { get; set; }
        public Nullable<double> Ширина { get; set; }
        public Nullable<double> Длина { get; set; }
        public Nullable<decimal> Цена { get; set; }
        public string Изображение { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ИзмерениеНоменклатуры> ИзмерениеНоменклатуры { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<СкладТкани> СкладТкани { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ТканиИзделия> ТканиИзделия { get; set; }
    }
}
