/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package poscodex;

import comcodex.Currency;
import comcodex.ServiceClientException;
import comcodex.Transaction;
import java.text.DecimalFormat;
import java.text.DecimalFormatSymbols;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.swing.JOptionPane;

/**
 *
 * @author rogerzavala
 */
public class DialogTransaction extends javax.swing.JDialog {

    private boolean isDebug = true;
    
    /**
     * Creates new form DialogTransaction
     */
    public DialogTransaction(java.awt.Frame parent, boolean modal) {
        super(parent, modal);
        initComponents();
    }

    DialogTransaction(MainFrame aThis) {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    /**
     * This method is called from within the constructor to initialize the form.
     * WARNING: Do NOT modify this code. The content of this method is always
     * regenerated by the Form Editor.
     */
    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        jLabel1 = new javax.swing.JLabel();
        buttonSave = new javax.swing.JButton();
        buttonCancel = new javax.swing.JButton();
        jLabel2 = new javax.swing.JLabel();
        jPanel1 = new javax.swing.JPanel();
        labelDevice = new javax.swing.JLabel();
        labelAmount = new javax.swing.JLabel();
        labelConcept = new javax.swing.JLabel();
        textboxDevice = new javax.swing.JTextField();
        textboxAmount = new javax.swing.JTextField();
        jScrollPane1 = new javax.swing.JScrollPane();
        textareaConcept = new javax.swing.JTextArea();

        jLabel1.setIcon(new javax.swing.ImageIcon(getClass().getResource("/poscodex/resources/logo_comcodex.png"))); // NOI18N

        setDefaultCloseOperation(javax.swing.WindowConstants.DISPOSE_ON_CLOSE);

        buttonSave.setFont(new java.awt.Font("Lucida Grande", 1, 13)); // NOI18N
        buttonSave.setText("GUARDAR");
        buttonSave.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                buttonSaveActionPerformed(evt);
            }
        });

        buttonCancel.setFont(new java.awt.Font("Lucida Grande", 1, 13)); // NOI18N
        buttonCancel.setText("CANCELAR");
        buttonCancel.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                buttonCancelActionPerformed(evt);
            }
        });

        jLabel2.setIcon(new javax.swing.ImageIcon(getClass().getResource("/poscodex/resources/logo_comcodex.png"))); // NOI18N

        jPanel1.setBorder(javax.swing.BorderFactory.createLineBorder(new java.awt.Color(153, 153, 153)));

        labelDevice.setFont(new java.awt.Font("Lucida Grande", 1, 13)); // NOI18N
        labelDevice.setText("Caja");

        labelAmount.setFont(new java.awt.Font("Lucida Grande", 1, 13)); // NOI18N
        labelAmount.setText("Monto");

        labelConcept.setFont(new java.awt.Font("Lucida Grande", 1, 13)); // NOI18N
        labelConcept.setText("Concepto");

        textareaConcept.setColumns(20);
        textareaConcept.setRows(5);
        jScrollPane1.setViewportView(textareaConcept);

        javax.swing.GroupLayout jPanel1Layout = new javax.swing.GroupLayout(jPanel1);
        jPanel1.setLayout(jPanel1Layout);
        jPanel1Layout.setHorizontalGroup(
            jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel1Layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(labelDevice)
                    .addComponent(labelAmount)
                    .addComponent(labelConcept))
                .addGap(29, 29, 29)
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                        .addComponent(textboxDevice)
                        .addComponent(textboxAmount, javax.swing.GroupLayout.DEFAULT_SIZE, 292, Short.MAX_VALUE))
                    .addComponent(jScrollPane1, javax.swing.GroupLayout.PREFERRED_SIZE, 584, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addContainerGap(22, Short.MAX_VALUE))
        );
        jPanel1Layout.setVerticalGroup(
            jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel1Layout.createSequentialGroup()
                .addGap(25, 25, 25)
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(labelDevice)
                    .addComponent(textboxDevice, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addGap(29, 29, 29)
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(labelAmount)
                    .addComponent(textboxAmount, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addGap(35, 35, 35)
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(labelConcept)
                    .addComponent(jScrollPane1, javax.swing.GroupLayout.PREFERRED_SIZE, 183, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addContainerGap(29, Short.MAX_VALUE))
        );

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, layout.createSequentialGroup()
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .addComponent(buttonCancel)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(buttonSave)
                .addGap(273, 273, 273))
            .addGroup(layout.createSequentialGroup()
                .addGap(14, 14, 14)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(jLabel2)
                    .addComponent(jPanel1, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addContainerGap(14, Short.MAX_VALUE))
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, layout.createSequentialGroup()
                .addContainerGap()
                .addComponent(jLabel2, javax.swing.GroupLayout.PREFERRED_SIZE, 91, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(jPanel1, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(buttonSave, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(buttonCancel, javax.swing.GroupLayout.PREFERRED_SIZE, 46, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addContainerGap(16, Short.MAX_VALUE))
        );

        pack();
    }// </editor-fold>//GEN-END:initComponents

    private void buttonCancelActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_buttonCancelActionPerformed
        // TODO add your handling code here:
        this.setVisible(false);
    }//GEN-LAST:event_buttonCancelActionPerformed

    
    /**
     * 
     * @param evt 
     */
    private void buttonSaveActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_buttonSaveActionPerformed
        
        DialogLoading loading = this.buildLoading();
        CustomWorker worker = new CustomWorker(CustomWorker.CREATE_TRANSACTION, this, loading );
        worker.execute();
        loading.setVisible(true);
    }//GEN-LAST:event_buttonSaveActionPerformed

  /**
     * 
     * @return 
     */
    public DialogLoading buildLoading()
    {
        DialogLoading loading = new DialogLoading( (MainFrame)this.getParent(), true);
        loading.setLocationRelativeTo(this);
        return loading;
    }
    
    
    /**
     * 
     */
    public void createTransaction() throws ServiceClientException
    {        
        MainFrame mainFrame = (MainFrame)this.getParent();
        
        Transaction transaction 	= new Transaction();
 
        transaction.amount          = this.checkAmount( this.textboxAmount.getText() );
        transaction.device          = this.textboxDevice.getText();
        transaction.concept 		= this.textareaConcept.getText();	
        
        try 
        {
            transaction = mainFrame.getServiceClient().openTransaction(transaction);
            mainFrame.setCurrentToken( transaction.token );
            mainFrame.showTransaction();
        } 
        catch (ServiceClientException ex) 
        {
            throw new ServiceClientException("Error al crear la transacción, cambie los parametros o espere");
        } 
    }
    
    

    /**
     * 
     * @param value
     * @return 
     */
    public Currency checkAmount( String value ) throws ServiceClientException
    {
        
        DecimalFormat format = (DecimalFormat)DecimalFormat.getInstance();
        DecimalFormatSymbols symbols = format.getDecimalFormatSymbols();
        char sep = symbols.getDecimalSeparator();

        String[] numericParts = value.split( String.valueOf("\\" + sep) );
        
        Currency currency = null;

        if( numericParts.length == 0) 
        {
            try
            {
                currency = new Currency( Integer.parseInt( value ), 0 );
            }
            catch( Exception ex)
            {
                throw new ServiceClientException("Error en el formato numerico. Debe ser del tipo 999" + sep + "99" );
            }
        }
        else if(   numericParts.length == 1 )
        {
            try
            {
                currency = new Currency(  Integer.parseInt( numericParts[0]), 0 );
            }
            catch( Exception ex)
            {
                 throw new ServiceClientException("Error en el formato numerico. Debe ser del tipo 999" + sep + "99" );
            }
        }
        else if(   numericParts.length  == 2 )
        {			
            try
            {
                currency = new Currency(  Integer.parseInt(  numericParts[0]),  Integer.parseInt( numericParts[1]) );
            }
            catch( Exception ex)
            {
                 throw new ServiceClientException("Error en el formato numerico. Debe ser del tipo 999" + sep + "99" );
            }
        }

        return currency;			
    }    
    
        /**
     * 
     * @param message
     * @param sourceMessage 
     */
    public void showError( String message, String sourceMessage )
    {
        if( this.isDebug )
            JOptionPane.showMessageDialog(this, sourceMessage); 
        else
            this.showError(message); 
    }
    
    
    /**
     * 
     * @param message 
     */
    public void showError( String message )
    {
       JOptionPane.showMessageDialog(this, message); 
    }    
    
    
    /**
     * @param args the command line arguments
     */
    public static void main(String args[]) {
        /* Set the Nimbus look and feel */
        //<editor-fold defaultstate="collapsed" desc=" Look and feel setting code (optional) ">
        /* If Nimbus (introduced in Java SE 6) is not available, stay with the default look and feel.
         * For details see http://download.oracle.com/javase/tutorial/uiswing/lookandfeel/plaf.html 
         */
        try {
            for (javax.swing.UIManager.LookAndFeelInfo info : javax.swing.UIManager.getInstalledLookAndFeels()) {
                if ("Nimbus".equals(info.getName())) {
                    javax.swing.UIManager.setLookAndFeel(info.getClassName());
                    break;
                }
            }
        } catch (ClassNotFoundException ex) {
            java.util.logging.Logger.getLogger(DialogTransaction.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (InstantiationException ex) {
            java.util.logging.Logger.getLogger(DialogTransaction.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (IllegalAccessException ex) {
            java.util.logging.Logger.getLogger(DialogTransaction.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (javax.swing.UnsupportedLookAndFeelException ex) {
            java.util.logging.Logger.getLogger(DialogTransaction.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        }
        //</editor-fold>

        /* Create and display the dialog */
        java.awt.EventQueue.invokeLater(new Runnable() {
            public void run() {
                DialogTransaction dialog = new DialogTransaction(new javax.swing.JFrame(), true);
                dialog.addWindowListener(new java.awt.event.WindowAdapter() {
                    @Override
                    public void windowClosing(java.awt.event.WindowEvent e) {
                        System.exit(0);
                    }
                });
                dialog.setVisible(true);
            }
        });
    }

    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JButton buttonCancel;
    private javax.swing.JButton buttonSave;
    private javax.swing.JLabel jLabel1;
    private javax.swing.JLabel jLabel2;
    private javax.swing.JPanel jPanel1;
    private javax.swing.JScrollPane jScrollPane1;
    private javax.swing.JLabel labelAmount;
    private javax.swing.JLabel labelConcept;
    private javax.swing.JLabel labelDevice;
    private javax.swing.JTextArea textareaConcept;
    private javax.swing.JTextField textboxAmount;
    private javax.swing.JTextField textboxDevice;
    // End of variables declaration//GEN-END:variables
}