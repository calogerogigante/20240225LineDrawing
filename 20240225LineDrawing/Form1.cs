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
                // on a cliqu� : testons � quelle �tape on �tait :
                if (etape_ligne == 0)
                {
                    // on est donc � la premi�re �tape de tracer une ligne
                    // on r�cup�re l'endroit du 1er clic
                    droiteEnCours.x1 = e.Location.X;
                    droiteEnCours.y1 = e.Location.Y;
                    // on r�cup�re aussi d�j� la position actuelle du curseur pour
                    // la placer dans x2,y2 de la droite en cours,
                    // sinon, le programme trace tr�s rapidement et bri�vement une ligne avec le point d'origine :
                    droiteEnCours.x2 = e.Location.X;
                    droiteEnCours.y2 = e.Location.Y;

                    etape_ligne = 1; // on est maintenant pass� � l'�tape suivante
                }
                else if (etape_ligne == 1)
                {
                    // on vient de cliquer sur le pictureBox1 :
                    // on est � la 2�me �tape de tracer une ligne
                    // on r�cup�re le 2�me clic dans la droite en cours de dessin
                    droiteEnCours.x2 = e.Location.X;
                    droiteEnCours.y2 = e.Location.Y;
                    // on ajoute la droite en cours � la liste des droites existantes :
                    drts.Add(new Droite(droiteEnCours.x1,droiteEnCours.y1, droiteEnCours.x2, droiteEnCours.y2));
                    etape_ligne = 0;
                    // on r�initialise la droite en cours :
                    droiteEnCours.reinitialiser();
                }
            }
            pictureBox1.Refresh();
            label_etape.Text = "Etape : " + etape_ligne.ToString();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            // Create pen.
            Pen monStylo = new Pen(Color.Black, 2); // �paisseur 2

            // au d�but, on trace toutes les droites de la liste des droites existantes :
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
                // on est � la premi�re �tape de tracer une ligne
                e.Graphics.DrawLine(monStylo, new Point(droiteEnCours.x1, droiteEnCours.y1),
                                              new Point(droiteEnCours.x2, droiteEnCours.y2));
            }
        }


        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            // si on est en attente du 2�me clic, il faut r�cup�rer la position  de la souris
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
