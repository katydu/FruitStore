using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//才可以使用display name
using System.ComponentModel;
//才可以使用required
using System.ComponentModel.DataAnnotations;

namespace FruitStore.Models
{
    public class MembersData
    {
        /// <summary>
        /// 會員編號
        /// </summary>
        [DisplayName("會員編號")]
        public int MemberId { get; set; }

        /// <summary>
        /// 會員名稱
        /// </summary>
        [DisplayName("會員名稱")]
        [Required(ErrorMessage = "必填")]
        public string MemberName { get; set; }

        /// <summary>
        /// 會員電子信箱
        /// </summary>
        [DisplayName("會員電子信箱")]
        [Required(ErrorMessage = "必填")]
        public string MemberEmail { get; set; }

        /// <summary>
        /// 會員地址
        /// </summary>
        [DisplayName("會員地址")]
        [Required(ErrorMessage = "必填")]
        public string MemberAddress { get; set; }

        /// <summary>
        /// 會員電話
        /// </summary>
        [DisplayName("會員電話")]
        [Required(ErrorMessage = "必填")]
        public string MemberPhone { get; set; }
    }
}