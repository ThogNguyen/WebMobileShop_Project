namespace PS36400_NguyenLocThong_Assignment.ViewModels
{
    public class CartVM
    {
        public int MaSp { get; set; }
        public string Hinh { get; set; }
        public string TenSp { get; set; }
        public int Giatien { get; set; }
        public int Soluong { get; set; }
        public double ThanhTien => Soluong * Giatien;
    }
}
