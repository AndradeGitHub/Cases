using System;
using System.Collections.Generic;

namespace worker_WalmartLayoutParser.catalogoProdutoWalmart
{
    public class RootObject
    {
        public string creationDate { get; set; }
        public List<Product> products { get; set; }
    }

    public class Product
    {
        public long? productId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public List<Category> categories { get; set; }
        public bool active { get; set; }
        public Brand brand { get; set; }
        public List<Attribute> attributes { get; set; }
        public List<Sku> skus { get; set; }
    }

    public class Brand
    {
        public int? id { get; set; }
        public string name { get; set; }
    }

    public class Category
    {
        public int? id { get; set; }
        public string name { get; set; }
    }

    public class Attribute
    {
        public string name { get; set; }
        public List<string> values { get; set; }
    }

    public class Sku
    {
        public int? id { get; set; }
        public string name { get; set; }
        public List<Offer> offers { get; set; }
        public List<Image> images { get; set; }
        public Dimensions dimensions { get; set; }
        public bool active { get; set; }
        public List<Specialization> specializations { get; set; }
        public List<string> eans { get; set; }
    }

    public class Dimensions
    {
        public int? height { get; set; }
        public int? length { get; set; }
        public int? weight { get; set; }
        public int? width { get; set; }
    }

    public class Offer
    {
        public Seller seller { get; set; }
        public Listprice listPrice { get; set; }
        public Discountprice discountPrice { get; set; }
        public bool active { get; set; }
        public bool available { get; set; }
    }

    public class Seller
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Listprice
    {
        public float BRL { get; set; }
    }

    public class Discountprice
    {
        public float BRL { get; set; }
    }

    public class Image
    {
        public string url { get; set; }
        public int? width { get; set; }
        public int? height { get; set; }
    }

    public class Specialization
    {
        public string name { get; set; }
        public List<string> values { get; set; }
    }
}