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
    
    public partial class Фурнитура_
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Фурнитура_()
        {
            this.ИзмерениеНоменклатуры = new HashSet<ИзмерениеНоменклатуры>();
            this.СкладФурнитуры = new HashSet<СкладФурнитуры>();
            this.ФурнитураИзделия = new HashSet<ФурнитураИзделия>();
        }
    
        public string Артикул { get; set; }
        public string Наименование { get; set; }
        public string Ширина { get; set; }
        public string Длина { get; set; }
        public string Тип { get; set; }
        public Nullable<double> Цена { get; set; }
        public Nullable<double> Вес { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ИзмерениеНоменклатуры> ИзмерениеНоменклатуры { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<СкладФурнитуры> СкладФурнитуры { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ФурнитураИзделия> ФурнитураИзделия { get; set; }
    }
}
