/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package serverudp;

import java.io.IOException;
import java.util.logging.Level;
import java.util.logging.Logger;
import static serverudp.DatiCondivisi.sElabora;

/**
 *
 * @author user
 */
public class ThElabora extends Thread{
    public void run(){
        while(!DatiCondivisi.Instance().termina)
        {
            try
            {
                
            sElabora.acquire();
            System.out.println("elabora");
                Messaggio daElaborare = DatiCondivisi.Instance().GetMessaggioRicevuti();
                if(daElaborare!=null)
                    DatiCondivisi.Instance().AddMessaggioDaInviare(DatiCondivisi.Instance().Elabora(daElaborare));
            } catch (IOException ex) {
            Logger.getLogger(ThRicevi.class.getName()).log(Level.SEVERE, null, ex);
            } catch (InterruptedException ex) {
                Logger.getLogger(ThElabora.class.getName()).log(Level.SEVERE, null, ex);
            }
        }
    }
}
