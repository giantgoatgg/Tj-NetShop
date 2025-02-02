﻿using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.ShopCenter
{
    public class ShopCenterDatabase
    {
        public static OracleConnection DB;

         //建立数据库连接
        public static void CreateConn()  //更改此处数据库地址即可
        {
            //124.222.1.19
            string user = "shop";
            string pwd = "jy2051914";
            string db = "124.222.1.19/helowin";
            string conStringUser = "User ID=" + user + ";password=" + pwd + ";Data Source=" + db + ";";
            //string connString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = orcl))); Persist Security Info=True;User ID=c##shop;Password=jinyi123mx427;";
            DB = new OracleConnection(conStringUser);
            DB.Open();
        }
         //关闭数据库连接
        public static void CloseConn()
        {
            DB.Close();
        }
        // 随机返回四个商品
        public static string GetFourRandomProduct()
        {
            List<Product_info> storage = new List<Product_info>();
            CreateConn();
            OracleCommand Search = DB.CreateCommand();

            Search.CommandText = "select name,img,price from (select * from product_infomation oreder by sys_guid()) where rownum<=4";
            OracleDataReader Ord = Search.ExecuteReader();
            while (Ord.Read())
            {
                Product_info product_info = new Product_info();
                product_info.name = Ord.GetValue(0).ToString();
                product_info.img = Ord.GetValue(1).ToString();
                product_info.price = Ord.GetValue(2).ToString();

                storage.Add(product_info);
            }
            //以字符串形式返回
            CloseConn();
            return JsonConvert.SerializeObject(storage);
        } 

        //返回店铺信息
        public static string getShopInfo(string shopId)
        {
            List<shop> storage = new List<shop>();
            CreateConn();
            OracleCommand Search = DB.CreateCommand();

            Search.CommandText = "select id, store_name, store_img, store_type_id, store_desc, create_time from shop where id =: shopId";
            Search.Parameters.Add(new OracleParameter(":shopId", shopId));
            OracleDataReader Ord = Search.ExecuteReader();

            while (Ord.Read())
            {
                shop Shop = new shop();
                Shop.shopId = Ord.GetValue(0).ToString();
                Shop.storeName = Ord.GetValue(1).ToString();
                Shop.storeImg = Ord.GetValue(2).ToString();
                Shop.storeTypeId = Ord.GetValue(3).ToString();
                Shop.storeDesc = Ord.GetValue(4).ToString();
                Shop.createTime = Ord.GetValue(5).ToString();

                storage.Add(Shop);
            }

            //以字符串形式返回
            CloseConn();
            return JsonConvert.SerializeObject(storage);
        }
    }
}
