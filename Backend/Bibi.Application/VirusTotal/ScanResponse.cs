namespace Bibi.Application.VirusTotal
{
    public class ScanResponse
    {
        public string Scan_id { get; set; }
        public string Sha1 { get; set; }
        public string Resource { get; set; }
        public string Response_code { get; set; }
        public string Sha256 { get; set; }
        public string Permalink { get; set; }
        public string Md5 { get; set; }
        public string Verbose_msg { get; set; }
    }
}