namespace AracKiralamaAPI.models
{
    public class Kiralama
    {
        public int id { get; set; }
        public int k_ID { get; set; }
        public int a_ID { get; set; }
        public DateTime baslangıc_tar { get; set; }
        public DateTime bitis_tar { get; set; }
        public string kiralanma_adresi { get; set; }
        public decimal toplam_ücret { get; set; }
    }
}
