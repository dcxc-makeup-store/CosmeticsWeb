//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace CosmeticsWeb.Models.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class 销售表
    {
        public int 销售编号 { get; set; }
        public string 商品编号 { get; set; }
        public int 销售数量 { get; set; }
        public decimal 销售单价 { get; set; }
        public System.DateTime 销售时间 { get; set; }
        public decimal 小计 { get; set; }
        public System.DateTime 失效期 { get; set; }
        public string 商品规格 { get; set; }
    
        public virtual 商品信息表 商品信息表 { get; set; }
    }
}
