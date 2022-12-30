using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FruitStore.Models
{
    public class Members
    {
        [Key]
        public int MemberID { get; set; }
        [Required]
        [DisplayName("姓名")]
        public string MemberName { get; set; }
        [Required]
        [DisplayName("電子信箱")]
        public string MemberEmail { get; set; }
        [Required]
        [DisplayName("居住地址")]
        public string MemberAddress { get; set; }
        [Required]
        [StringLength(10,ErrorMessage ="請輸入正確的電話格式")]
        [DisplayName("電話號碼")]
        public string MemberPhone { get; set; }
        [Required]
        [DisplayName("密碼")]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{6,30}$",ErrorMessage ="密碼不符合規定")]
        public string MemberPassword { get; set; }

        // add ? to set this class nullable, else it will get error at create new member
        public ICollection<Orders>? Orders { get; set; }
    }

    public class Orders
    {
        [Key]
        public int OrderID { get; set; }
        public DateTime OrderTime { get; set; }
        public int OrderFee { get; set; }
        public string OrderAddress { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }

    public class Products
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int ProductUnitPrice { get; set; }
        //TODO later need to delete this prop
        public int? TempQuantity { get; set; }
        public string? ProductImg { get; set; }

        public ICollection<OrderDetails> OrderDetails { get; set; }
    }

    public class OrderDetails
    {
        [Key]
        public int OrderDetailNum { get; set; }
        public int Quantity { get; set; }
        public int OrderTotalPrice { get; set; }
        public Orders Orders { get; set; }
        public Products Products { get; set; }
    }
}

