/***
* El siguiente programa en processing permite hacerle tracking a dos Marcadores pasivos
* que tengan colores diferentes.
*/
// importa la librería video
import processing.video.*;
import processing.serial.*;
import processing.net.*;
import netP5.*;
import oscP5.*;


//Instancio la variable para capturar la camara
Capture camara;
Server servidor;
String datosPosiciones = "";//Guarda la informacion que se enviara por el puerto.

// Instancio la variable del color que se va a buscar
color marcadorA;
color marcadorB;

int xMarcadorA = 0;
int yMarcadorA = 0;

int xMarcadorB = 0;
int yMarcadorB = 0;

// Distancia de semejanza en color
float semejanzaEnColor = 45;
float minimoDePixelesSemejantes = 50;

String ip = "190.99.197.16"; // Cambia esto con la dirección IP de tu dispositivo
int port = 12000; // Puerto de comunicación

OscP5 oscP5;
NetAddress remoteLocation;
float x, y;


void settings() {
    // creo la ventana
    size(640, 480);
}

void setup()
{
  /*  // Inicializar la librería Ketai
    sensor = new KetaiSensor(this);
    cam = new KetaiCamera(this, width, height, 30);
    cam.start();
  */
    // Iniciar servidor en el puerto 5204
    servidor = new Server(this, 5204);

    // Habilitar solo para depurar el driver de la camara en caso de problemas detectando la camara
    //String[] cameras = Capture.list();
    //printArray(cameras);
    //camara = new Capture(this, cameras[3]);

    //En la variable video almaceno la camra
    camara = new Capture(this, width, height, 30);
    camara.start();

    // Marcadores
    marcadorA = color(154.0,174.0,58.0); // Color real del marcador A.
    marcadorB = color(255,60,79); // Color real del marcador B.
    
    // Configura la comunicación OSC
    oscP5 = new OscP5(this, 5204);
    remoteLocation = new NetAddress("190.99.198.250", 5204); 
    // Coloca aquí la dirección IP de tu celular

}

void draw()
{

    
    //background(255);
    // Dibuja un círculo en la posición enviada por el celular
    //ellipse(mouseX, mouseY, 50, 50);
    
    //verifica si la camara esta disponible
    if (camara.available())
    {
        camara.read();
        image(camara, 0, 0);
        camara.loadPixels();
        
        /*rect (0,0,213,160);//VERDE CENTRO
        fill(255, 0, 0, 150);
        
        rect (213,160,213,160);//NARANJA
        fill(255, 150, 0, 150);
        
        rect (426,320,213,160);//AZUL 
        fill(0, 110, 255, 150);
        
        rect (0,320,213,160);//cuadrado iz down
        fill(110, 180, 0, 150);
        
        rect (426,0,213,160);//cuadrado der down
        fill(0, 0, 250, 50);*/

        float promedioXMarcadorA = 0;
        float promedioYMarcadorA = 0;

        float promedioXMarcadorB = 0;
        float promedioYMarcadorB = 0;

        int cantidadPixelesCoincidenConMarcadorA = 0;
        int cantidadPixelesCoincidenConMarcadorB = 0;
                       
        //empieza a recorrer cada pixel
        for ( int x = 0; x < camara.width; x++ )
        {
            for ( int y = 0; y < camara.height; y++ )
            {

                color pixelActual = camara.pixels[x + y * camara.width];

                float cantidadRojoDelPixelActual = red(pixelActual);
                float cantidadVerdeDelPixelActual = green(pixelActual);
                float cantidadAzulDelPixelActual = blue(pixelActual);

                float cantidadRojoDelMarcadorA = red(marcadorA);
                float cantidadVerdeDelMarcadorA = green(marcadorA);
                float cantidadAzulDelMarcadorA = blue(marcadorA);

                // Calculando la distancia de similitud en "color" para el marcador A.
                float similitudEnDistanciaDelColorMarcadorA = dist(cantidadRojoDelPixelActual, cantidadVerdeDelPixelActual, cantidadAzulDelPixelActual, cantidadRojoDelMarcadorA, cantidadVerdeDelMarcadorA, cantidadAzulDelMarcadorA); // We are using the dist( ) function to compare the current color with the color we are tracking.

                // Esta muy cerca del rojo
                if (similitudEnDistanciaDelColorMarcadorA < semejanzaEnColor)
                {
                    promedioXMarcadorA += x;
                    promedioYMarcadorA += y;
                    cantidadPixelesCoincidenConMarcadorA++;
                } else {

                    float cantidadRojoDelMarcadorB = red(marcadorB);
                    float cantidadVerdeDelMarcadorB = green(marcadorB);
                    float cantidadAzulDelMarcadorB = blue(marcadorB);

                    float similitudEnDistanciaDelColorMarcadorB = dist(cantidadRojoDelPixelActual, cantidadVerdeDelPixelActual, cantidadAzulDelPixelActual, cantidadRojoDelMarcadorB, cantidadVerdeDelMarcadorB, cantidadAzulDelMarcadorB); // We are using the dist( ) function to compare the current color with the color we are tracking.

                    // Esta muy cerca del verde
                    if (similitudEnDistanciaDelColorMarcadorB < semejanzaEnColor)
                    {
                    promedioXMarcadorB += x;
                    promedioYMarcadorB += y;
                    cantidadPixelesCoincidenConMarcadorB++;
                    }

                }
            }
        }

        if ( cantidadPixelesCoincidenConMarcadorA > minimoDePixelesSemejantes )
        {
            xMarcadorA = (int) promedioXMarcadorA / cantidadPixelesCoincidenConMarcadorA;
            yMarcadorA = (int) promedioYMarcadorA / cantidadPixelesCoincidenConMarcadorA;
        }

        if ( cantidadPixelesCoincidenConMarcadorB > minimoDePixelesSemejantes )
        {
            xMarcadorB = (int) promedioXMarcadorB / cantidadPixelesCoincidenConMarcadorB;
            yMarcadorB = (int) promedioYMarcadorB / cantidadPixelesCoincidenConMarcadorB;
        }

        dibujarCentroide(marcadorA, xMarcadorA, yMarcadorA);
        //dibujarCentroide(marcadorB, xMarcadorB, yMarcadorB);

        if (xMarcadorA > 0 || yMarcadorA > 0 || xMarcadorB > 0 || yMarcadorB > 0) {
            datosPosiciones = (width-xMarcadorA)+","+(height-yMarcadorA)+","+(width-xMarcadorB)+","+(height-yMarcadorB)+"\n";
        } else {
            datosPosiciones = "0,0,0,0\n";
        }
        //Enviar el dato por el puerto
        servidor.write(datosPosiciones);
    }
    

}

/**
* Dibujar centroide.
*/
void dibujarCentroide (color marcador, int xMarcador, int yMarcador) {
    fill(marcador);
    strokeWeight(4.0);
    stroke(0);
    ellipse(xMarcador, yMarcador, 16, 16);
  }

/**
* Obtiene el color exacto del pixel donde
* se dio un click.
*/
void mousePressed() {

    int loc = mouseX + mouseY * camara.width;
    color pixelLeido = camara.pixels[loc];

    float r1 = red(pixelLeido);
    float g1 = green(pixelLeido);
    float b1 = blue(pixelLeido);
    print(r1 + " "+ g1+ " "+b1+ "\n");

    //Habilitar para hacer tracking
    //marcadorRojo = pixelLeido;
}

void oscEvent(OscMessage message) {
  if (message.checkAddrPattern("/posicion")) {
    float x = message.get(0).floatValue();
    float y = message.get(1).floatValue();
    mouseX = int(map(x, 0, 1, 0, width));
    mouseY = int(map(y, 0, 1, 0, height));
 }
}
