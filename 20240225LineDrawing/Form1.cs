namespace _20240225LineDrawing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<Droite> drts = new List<Droite>();
        public int etape_ligne = 0;

        public Droite droiteEnCours = new Droite();

        private void Form1_Load(object sender, EventArgs e)
        {
            label_etape.Text = "Etape : " + etape_ligne.ToString();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // on a cliqué : testons à quelle étape on était :
                if (etape_ligne == 0)
                {
                    // on est donc à la première étape de tracer une ligne
                    // on récupère l'endroit du 1er clic
                    droiteEnCours.x1 = e.Location.X;
                    droiteEnCours.y1 = e.Location.Y;
                    // on récupère aussi déjà la position actuelle du curseur pour
                    // la placer dans x2,y2 de la droite en cours,
                    // sinon, le programme trace très rapidement et brièvement une ligne avec le point d'origine :
                    droiteEnCours.x2 = e.Location.X;
                    droiteEnCours.y2 = e.Location.Y;

                    etape_ligne = 1; // on est maintenant passé à l'étape suivante
                }
                else if (etape_ligne == 1)
                {
                    // on vient de cliquer sur le pictureBox1 :
                    // on est à la 2ème étape de tracer une ligne
                    // on récupère le 2ème clic dans la droite en cours de dessin
                    droiteEnCours.x2 = e.Location.X;
                    droiteEnCours.y2 = e.Location.Y;
                    // on ajoute la droite en cours à la liste des droites existantes :
                    drts.Add(new Droite(droiteEnCours.x1,droiteEnCours.y1, droiteEnCours.x2, droiteEnCours.y2));
                    etape_ligne = 0;
                    // on réinitialise la droite en cours :
                    droiteEnCours.reinitialiser();
                }
            }
            pictureBox1.Refresh();
            label_etape.Text = "Etape : " + etape_ligne.ToString();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            // Create pen.
            Pen monStylo = new Pen(Color.Black, 2); // épaisseur 2

            // au début, on trace toutes les droites de la liste des droites existantes :
            foreach (Droite d in drts)
            {
                // Create points that define line.
                e.Graphics.DrawLine(monStylo, new Point(d.x1, d.y1), new Point(d.x2, d.y2));
            }

            if (etape_ligne == 0) // on ne trace pas de nouvelle droite
            {

            }
            else if (etape_ligne == 1)
            {
                float[] dashValues = { 8, 2};
                monStylo = new Pen(Color.Red, 2);
                monStylo.DashPattern = dashValues;
                // on est à la première étape de tracer une ligne
                e.Graphics.DrawLine(monStylo, new Point(droiteEnCours.x1, droiteEnCours.y1),
                                              new Point(droiteEnCours.x2, droiteEnCours.y2));
            }
        }


        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            // si on est en attente du 2ème clic, il faut récupérer la position  de la souris
            // pour pouvoir tracer la droite en cours, quand on est en train de bouger la souris :
            if (etape_ligne == 1)
            {
                droiteEnCours.x2 = e.Location.X;
                droiteEnCours.y2 = e.Location.Y;
                pictureBox1.Refresh();
            }
        }
    }
}
