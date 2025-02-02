﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.MallPage
{
    public class MallPageModel
    {
        public string Get_4_Random_Product()
        {
            return MallPageDatabase.GetFourRandomProduct();
        }
        public string  Get_4_Collected_Product(int userid)
        {
            return GetFourCollectedProduct(userid);
        }
        public string Get_4_Discount_Product()
        {
            return GetFourDiscountProduct();
        }
        public string Get_Random_Shop_Product()
        {
            return GetRandomShopProduct();
        }
     }
}
