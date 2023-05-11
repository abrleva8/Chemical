namespace Don_tKnowHowToNameThis; 

public class Material {
    
    public double P { get; set; } //1060
    public double C { get; set; } //1200
    public double T0 { get; set; } //175

    public Material(double p, double c, double t0) {
        P = 1060;
        C = 1200;
        T0 = 175;
    }
    
    public Material() {}
}