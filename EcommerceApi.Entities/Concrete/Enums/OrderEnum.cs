namespace EcommerceApi.Entities.Concrete.Enums
{
    //Sifaris hansi merhelede oldugunu gosterir 
    public enum OrderEnum
    {
        OnPending, //Gozlemede
        Shipped, //Göndərilib
        Arrived,  //Çatıb
        Complited, //Tamamlanıb
        Returned, //Qaytarılıb
        Canceled //Ləğv edilib
    }
}
