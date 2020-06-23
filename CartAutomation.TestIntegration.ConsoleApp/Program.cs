using CartAutomation.Business.CartOperation;
using CartAutomation.Business.Core.DependencyResolves.Ninject;
using CartAutomation.Business.DataOperation.Abstract;
using CartAutomation.Business.DataOperation.Concrete;
using CartAutomation.Data.DataAccess.Abstract;
using CartAutomation.Data.DataAccess.Concrete.EntityFramework;
using CartAutomation.Data.Entities.Concrete;
using CartAutomation.Data.Entities.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartAutomation.TestIntegration.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var categoryService = InstanceFactory.GetInstance<ICategoryService>();
            var productService = InstanceFactory.GetInstance<IProductService>();
            var couponService = InstanceFactory.GetInstance<ICouponService>();
            var campaignService = InstanceFactory.GetInstance<ICampaignService>();

            #region veritabanini doldur
            //InsertCategoryAndProducts(categoryService, productService);

            //InsertCoupons(couponService);

            //InsertCampaign(campaignService, categoryService);
            #endregion

            if (categoryService.GetByName("Kitap") != null)
            {
                Cart myCart = new Cart();
                myCart.UserId = 1;
                //her case için shopping metodu yazılır
                //bazı örnek caseler için shopping3 tanımlı olduğu regiona gidin
                shopping5(myCart, productService, categoryService);
                Console.WriteLine("*** Ürünler Sepete Eklendi ***");

                CartManager cartManager = new CartManager();
                Order order = cartManager.SaveCart(myCart);
                cartManager.DiscountCalculator = new CampaignDiscountCalculator();
                cartManager.ApplyDiscount(order);
                cartManager.DiscountCalculator = new CouponDiscountCalculator();
                cartManager.ApplyDiscount(order);
                cartManager.DeliveryCostCalculator = new StandardDeliveryCostCalculator(2, 1, (decimal)2.99);
                cartManager.ApplyDeliveryCost(order);
                order.State = 2;// tahsilata hazır
                cartManager.SetOrderForPayment(order);
                Console.WriteLine("*** Sepet Kaydedildi ***");
                Console.WriteLine("*********************************************************************");
                Console.WriteLine("*** Sipariş Özeti ***");
                Console.WriteLine(JsonConvert.SerializeObject(cartManager.GetOrderSummary(order)));
                Console.WriteLine("*** Sipariş Detay ***");
                Console.WriteLine(JsonConvert.SerializeObject(cartManager.GetOrderDetailSummary(order)));
            }else
            {
                Console.WriteLine("!!! Önce Database'i doldur.Region veritabanini dolduru aç !!!");
            }
            
            Console.ReadLine();
        }
        #region shoppingMethods TestCase
        //case1: hiç indirim yok
        public static void shopping1(Cart cart,IProductService productService, ICategoryService categoryService)
        {
            cart.cartItemList = new List<CartItem>();
            CartItem cartItem = new CartItem();
            cartItem.Product = productService.GetByName("Kablo");
            cartItem.Quantity = 2;
            cart.cartItemList.Add(cartItem);
        }
        //case2: sadece kupon indirimi(sepette tek tip ürün var)(indirim doğru uygulanıyor mu)
        public static void shopping2(Cart cart, IProductService productService, ICategoryService categoryService)
        {
            cart.cartItemList = new List<CartItem>();
            CartItem cartItem = new CartItem();
            cartItem.Product = productService.GetByName("GeneralMobile");
            cartItem.Quantity = 1;
            cart.cartItemList.Add(cartItem);
        }
        //case3: (kampanya(tek kategoride tanımlı)+kupan)(sepette farklı tip ürün var)(indirim orderdetail'da doğru paylaştırılıyor mu)
        public static void shopping3(Cart cart, IProductService productService, ICategoryService categoryService)
        {
            cart.cartItemList = new List<CartItem>();
            CartItem cartItem = new CartItem();
            cartItem.Product = productService.GetByName("Elbise");
            cartItem.Quantity =2;
            cart.cartItemList.Add(cartItem);
            CartItem ct = new CartItem();
            ct.Product = productService.GetByName("Mont");
            ct.Quantity = 1;
            cart.cartItemList.Add(ct);
        }
        //case4: sadece tek kampanya (kampanya rate tanımlı)
        public static void shopping4(Cart cart, IProductService productService, ICategoryService categoryService)
        {
            cart.cartItemList = new List<CartItem>();
            CartItem cartItem = new CartItem();
            cartItem.Product = productService.GetByName("Hamlet");
            cartItem.Quantity = 3;
            cart.cartItemList.Add(cartItem);
            CartItem ct = new CartItem();
            ct.Product = productService.GetByName("SanatNedir");
            ct.Quantity = 2;
            cart.cartItemList.Add(ct);
        }
        //case5: birden fazla kampanya 
        public static void shopping5(Cart cart, IProductService productService, ICategoryService categoryService)
        {
            cart.cartItemList = new List<CartItem>();
            CartItem cartItem = new CartItem();
            cartItem.Product = productService.GetByName("Secret");
            cartItem.Quantity = 9;
            cart.cartItemList.Add(cartItem);
            CartItem ct = new CartItem();
            ct.Product = productService.GetByName("SanatNedir");
            ct.Quantity = 2;
            cart.cartItemList.Add(ct);
        }
        #endregion

        #region InsertCampaign
        public static void InsertCampaign(ICampaignService campaignService, ICategoryService categoryService)
        {
            InsertCampaignItem(campaignService, categoryService.GetByName("Kitap").Id ,5, 10, default(decimal),DiscountType.RATE);
            InsertCampaignItem(campaignService, categoryService.GetByName("Kitap").Id, 10, default(int), 20, DiscountType.AMOUNT);
            InsertCampaignItem(campaignService, categoryService.GetByName("Giyim").Id, 3, default(int), 30, DiscountType.AMOUNT);
            Console.WriteLine("*** Kampanyalar Tanımlandı ***");
        }
        public static void InsertCampaignItem(ICampaignService campaignService,int categoryId, int minNumberOfProduct, int rate, decimal discountAmount, DiscountType discountType)
        {
            Campaign campaign = new Campaign();
            campaign.CategoryId = categoryId;
            campaign.MinNumberOfProduct = minNumberOfProduct;
            campaign.Rate = rate;
            campaign.DiscountAmount = discountAmount;
            campaign.DiscountType = (int)discountType;
            campaignService.Add(campaign);
        }
        #endregion

        #region InsertCoupons
        public static void InsertCoupons(ICouponService couponService)
        {
            InsertCouponItem(couponService, 120, default(int), 15, DiscountType.AMOUNT);
            Console.WriteLine("*** Kupon Tanımlandı ***");
        }
        public static void InsertCouponItem(ICouponService categorytService, decimal minAmountLimit, int rate, decimal discountAmount, DiscountType discountType)
        {
            Coupon coupon = new Coupon();
            coupon.MinAmountLimit = minAmountLimit;
            coupon.Rate =rate;
            coupon.DiscountAmount = discountAmount;
            coupon.DiscountType = (int)discountType;
            categorytService.Add(coupon);
        }
        #endregion

        #region InsertCategoryAndProducts
        public static void InsertCategoryAndProducts(ICategoryService categorytService, IProductService productService)
        {
            int kitapCategoryId = InsertCategoryItem(categorytService, "Kitap");
            int elektronikCategoryId = InsertCategoryItem(categorytService, "Elektronik");
            int giyimCategoryId = InsertCategoryItem(categorytService, "Giyim");
            Console.WriteLine("*** Veritabanına Katageoriler Eklendi ***");

            InsertProductItem(productService, "MasterAlgoritma", kitapCategoryId, 25);
            InsertProductItem(productService, "Hamlet", kitapCategoryId, 15);
            InsertProductItem(productService, "SanatNedir", kitapCategoryId, 20);
            InsertProductItem(productService, "Secret", kitapCategoryId, 2);

            InsertProductItem(productService, "AppleTelefon", elektronikCategoryId, 8000);
            InsertProductItem(productService, "SamsungTelefon", elektronikCategoryId, 2500);
            InsertProductItem(productService, "GeneralMobile", elektronikCategoryId, 2000);
            InsertProductItem(productService, "OpppTelefon", elektronikCategoryId, 2250);
            InsertProductItem(productService, "Kablo", elektronikCategoryId, 10);

            InsertProductItem(productService, "Elbise", giyimCategoryId, 150);
            InsertProductItem(productService, "Mont", giyimCategoryId, 350);
            InsertProductItem(productService, "Yelek", giyimCategoryId, 120);
            InsertProductItem(productService, "Gomlek", giyimCategoryId, 100);
            Console.WriteLine("*** Veritabanına Ürünler Eklendi ***");
        }
        public static int InsertCategoryItem(ICategoryService categorytService, string name)
        {
            Category category = new Category();
            category.Name = name;
            return categorytService.Add(category);
        }
        public static void InsertProductItem(IProductService productService, string name, int categoryId, decimal price)
        {
            Product product = new Product();
            product.Name = name;
            product.CategoryId = categoryId;
            product.Price = price;
            productService.Add(product);
        }
        #endregion
    }
}
