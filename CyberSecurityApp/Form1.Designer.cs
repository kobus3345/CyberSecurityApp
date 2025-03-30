namespace CyberSecurityApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label1 = new Label();
            questionBox = new TextBox();
            naswerButton = new Button();
            answerBox = new TextBox();
            clearButton = new Button();
            clearButton2 = new Button();
            closeButton = new Button();
            timer1 = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // label1
            // 
            label1.BackColor = Color.Black;
            label1.BorderStyle = BorderStyle.FixedSingle;
            label1.CausesValidation = false;
            resources.ApplyResources(label1, "label1");
            label1.ForeColor = SystemColors.HotTrack;
            label1.Name = "label1";
            // 
            // questionBox
            // 
            questionBox.BorderStyle = BorderStyle.FixedSingle;
            questionBox.ForeColor = SystemColors.HotTrack;
            resources.ApplyResources(questionBox, "questionBox");
            questionBox.Name = "questionBox";
            // 
            // naswerButton
            // 
            resources.ApplyResources(naswerButton, "naswerButton");
            naswerButton.ForeColor = Color.Lime;
            naswerButton.Name = "naswerButton";
            naswerButton.UseVisualStyleBackColor = true;
            naswerButton.Click += naswerButton_Click;
            // 
            // answerBox
            // 
            answerBox.BackColor = Color.Black;
            resources.ApplyResources(answerBox, "answerBox");
            answerBox.ForeColor = Color.Lime;
            answerBox.Name = "answerBox";
            // 
            // clearButton
            // 
            clearButton.BackColor = Color.Black;
            resources.ApplyResources(clearButton, "clearButton");
            clearButton.ForeColor = Color.Lime;
            clearButton.Name = "clearButton";
            clearButton.UseVisualStyleBackColor = false;
            clearButton.Click += clearButton_Click;
            // 
            // clearButton2
            // 
            clearButton2.BackColor = Color.Black;
            resources.ApplyResources(clearButton2, "clearButton2");
            clearButton2.ForeColor = Color.Lime;
            clearButton2.Name = "clearButton2";
            clearButton2.UseVisualStyleBackColor = false;
            clearButton2.Click += clearButton2_Click;
            // 
            // closeButton
            // 
            closeButton.BackColor = Color.Black;
            resources.ApplyResources(closeButton, "closeButton");
            closeButton.ForeColor = Color.Chartreuse;
            closeButton.Name = "closeButton";
            closeButton.UseVisualStyleBackColor = false;
            closeButton.Click += closeButton_Click;
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Lime;
            Controls.Add(closeButton);
            Controls.Add(clearButton2);
            Controls.Add(clearButton);
            Controls.Add(answerBox);
            Controls.Add(naswerButton);
            Controls.Add(questionBox);
            Controls.Add(label1);
            Cursor = Cursors.Hand;
            Name = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox questionBox;
        private Button naswerButton;
        private TextBox answerBox;
        private Button clearButton;
        private Button clearButton2;
        private Button closeButton;
        private System.Windows.Forms.Timer timer1;
    }
}
