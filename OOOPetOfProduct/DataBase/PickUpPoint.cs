//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OOOPetOfProduct.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class PickUpPoint
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PickUpPoint()
        {
            this.Orders = new HashSet<Order>();
        }
    
        public int PickUpPointID { get; set; }
        public string PickUpPointNumber { get; set; }
        public string PickUpPointCIty { get; set; }
        public string PickUpPointStreet { get; set; }
        public string PickUpPointNumberStreet { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
