/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package serverudp;

import java.util.Scanner;

/**
 *
 * @author user
 */
public class ServerUdp {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        // TODO code application logic here
        new ThInvia().start();
        new ThRicevi().start();
        new ThElabora().start();
        new ThElabora().start();
        Scanner s=new Scanner(System.in);
        while (!DatiCondivisi.Instance().termina)
        {
            System.out.println("Inserisci exit per terminare il programma");
            DatiCondivisi.Instance().termina = s.next().toUpperCase().equals( "exit".toUpperCase());
        }
    }
    
}
