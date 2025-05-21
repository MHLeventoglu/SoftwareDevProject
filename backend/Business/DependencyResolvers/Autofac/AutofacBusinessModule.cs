using System;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;

using Business.Concrete.Orders;
using Business.Concrete.Products;
using Business.Concrete.Users;
using Business.Concrete.Preferences;

using Business.Abstract.Orders;
using Business.Abstract.Products;
using Business.Abstract.Users;
using Business.Abstract.Preferences;

using DataAccess.Abstract.Orders;
using DataAccess.Abstract.Products;
using DataAccess.Abstract.Users;
using DataAccess.Abstract.Preferences;

using DataAccess.Concrete.EntityFramework.Orders;
using DataAccess.Concrete.EntityFramework.Products;
using DataAccess.Concrete.EntityFramework.Users;
using DataAccess.Concrete.EntityFramework.Preferences;

namespace Business.DependencyResolvers.Autofac;

public class AutofacBusinessModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        // This method is called by the runtime to register services with the Autofac container.
        // And registers every service as a singltone instance.
        // By doing this, we are being respected to the singleton design pattern
        // Orders
        builder.RegisterType<CartManager>().As<ICartService>().SingleInstance();
        builder.RegisterType<CartItemManager>().As<ICartItemService>().SingleInstance();
        builder.RegisterType<OrderManager>().As<IOrderService>().SingleInstance();
        builder.RegisterType<PaymentManager>().As<IPaymentService>().SingleInstance();

        builder.RegisterType<EfCartDal>().As<ICartDal>().SingleInstance();
        builder.RegisterType<EfCartItemDal>().As<ICartItemDal>().SingleInstance();
        builder.RegisterType<EfOrderDal>().As<IOrderDal>().SingleInstance();
        builder.RegisterType<EfPaymentDal>().As<IPaymentDal>().SingleInstance();

        // Products
        builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();
        builder.RegisterType<CategoryManager>().As<ICategoryService>().SingleInstance();
        builder.RegisterType<BrandManager>().As<IBrandService>().SingleInstance();
        builder.RegisterType<DiscountManager>().As<IDiscountService>().SingleInstance();

        builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();
        builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().SingleInstance();
        builder.RegisterType<EfBrandDal>().As<IBrandDal>().SingleInstance();
        builder.RegisterType<EfDiscountDal>().As<IDiscountDal>().SingleInstance();

        // Users
        builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();
        builder.RegisterType<CustomerManager>().As<ICustomerService>().SingleInstance();
        builder.RegisterType<StaffManager>().As<IStaffService>().SingleInstance();
        builder.RegisterType<StaffTypeManager>().As<IStaffTypeService>().SingleInstance();

        builder.RegisterType<EfUserDal>().As<IUserDal>().SingleInstance();
        builder.RegisterType<EfCustomerDal>().As<ICustomerDal>().SingleInstance();
        builder.RegisterType<EfStaffDal>().As<IStaffDal>().SingleInstance();
        builder.RegisterType<EfStaffTypeDal>().As<IStaffTypeDal>().SingleInstance();

        // Preferences
        builder.RegisterType<AddressManager>().As<IAddressService>().SingleInstance();
        builder.RegisterType<WishlistManager>().As<IWishlistService>().SingleInstance();

        builder.RegisterType<EfAddressDal>().As<IAddressDal>().SingleInstance();
        builder.RegisterType<EfWishlistDal>().As<IWishlistDal>().SingleInstance();

        var assembly = System.Reflection.Assembly.GetExecutingAssembly();

        builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
            .EnableInterfaceInterceptors(new ProxyGenerationOptions() { Selector = new AspectInterceptorSelector() }).SingleInstance();
    }
}
