using System;

namespace Don_tKnowHowToNameThis {
    public class Calc {
        public const double R = 8.314; //ok
        public readonly double W = 0.20; //ok 
        public readonly double H = 0.009; //ok
        public readonly double L = 4.5; //ok
        // TODO: make the step variable
        public readonly double Step = 0.1;
        
        public double P = 1060; //ok
        public double C = 1200; //ok
        public double T0 = 175; //ok

        // public Material material = new Material(_P, _C, _T0);
        public Material Material;
        
        
        
        public readonly double Vu = 1.2; //ok
        public readonly double Tu = 220; //ok
        public readonly double Mu0 = 9000; //ok
        public readonly double Ea = 92000; //ok
        public readonly double Tr = 210; //ok
        public readonly double N = 0.3; //ok
        public readonly double AlphaU = 450; //ok

        public readonly double WMin = 0.0001;
        public readonly double HMin = 0.0001;
        public readonly double LMin = 0.01;
        public readonly double StepMin = 0.05;
        public readonly double PMin = 100;
        public readonly double CMin = 100;
        public readonly double T0Min = 30;
        public readonly double VuMin = 0.01;
        public readonly double TuMin = 30;
        public readonly double Mu0Min = 100;
        public readonly double EaMin = 100;
        public readonly double TrMin = 30;
        public readonly double NMin = 0.01;
        public readonly double AlphaUMin = 10;

        public readonly double WMax = 3;
        public readonly double HMax = 3;
        public readonly double LMax = 10;
        public readonly double StepMax = 2;
        public readonly double PMax = 20000;
        public readonly double CMax = 10000;
        public readonly double T0Max = 1000;
        public readonly double VuMax = 10;
        public readonly double TuMax = 1000;
        public readonly double Mu0Max = 50000;
        public readonly double EaMax = 1000000;
        public readonly double TrMax = 1000;
        public readonly double NMax = 1;
        public readonly double AlphaUMax = 5000;


        private double _gammaPoint;
        private double _qGamma;
        private double _beta;
        private double _qAlpha;
        private double _f;
        private double _qch;
        public double Q;

        public Calc(double w, double h, double l, double step, double p, double c, double t0, double vu, double tu, double mu0, double ea, double tr, double n, double alphaU) {
            W = w;
            H = h;
            L = l;
            Step = step;
            Material = new (p, c, t0);
            // P = p;
            // C = c;
            // T0 = t0;
            Vu = vu;
            Tu = tu;
            Mu0 = mu0;
            Ea = ea;
            Tr = tr;
            N = n;
            AlphaU = alphaU;
        }

        public Calc() {
            Material = new Material();
        }

        public Calc(Material material) {
            Material = material;
        }

        public void MaterialShearStrainRate() {
            _gammaPoint = Vu / H;
        }
        public void SpecificHeatFluxes() {
            _qGamma = H * W * Mu0 * Math.Pow(_gammaPoint, N + 1);
            _beta = Ea / (R * (Material.T0 + 20 + 273) * (Tr + 273));
            _qAlpha = W * AlphaU * (Math.Pow(_beta, -1) - Tu + Tr);
        }
        public void VolumeFlowRateOfMaterialFlowInTheChannel() {
            _f = 0.125 * Math.Pow(H / W, 2) - 0.625 * (H / W) + 1;
            _qch = H * W * Vu * _f / 2;
        }

        public double Temperature(double z) {
            return Tr + (1 / _beta) * Math.Log((_beta * _qGamma + W * AlphaU) / (_beta * _qAlpha) * (1 - Math.Exp((-_beta * _qAlpha * z) / (Material.P * Material.C * _qch)))
                                                + Math.Exp(_beta * (Material.T0 - Tr - (_qAlpha * z) / (Material.P * Material.C * _qch))));
        }
        public double Viscosity(double T) {
            return Mu0 * Math.Exp(-_beta * (T - Tr)) * Math.Pow(_gammaPoint, N - 1);
        }
        public double Efficiency() {
            Q = Math.Round(Material.P * _qch, 2);
            return Q;
        }
    }
}
