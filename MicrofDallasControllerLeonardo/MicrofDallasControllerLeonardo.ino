// Include the libraries we need
#include <OneWire.h>
#include <DallasTemperature.h>

// Data wire is plugged into port 2 on the Arduino
#define ONE_WIRE_BUS A1

#define DefaultResolution 11

#define SN_Length 21
#define AuthKey_Length 10

//#define UseFilter

#ifdef UseFilter
  #include "GyverFilters.h"
  GMedian<10, float> Filter;
#endif
// Setup a oneWire instance to communicate with any OneWire devices (not just Maxim/Dallas temperature ICs)
OneWire oneWire(ONE_WIRE_BUS);

// Pass our oneWire reference to Dallas Temperature. 
DallasTemperature sensors(&oneWire);

// arrays to hold device address
DeviceAddress insideThermometer[8];

long lastUpdateTime = 0; // Переменная для хранения времени последнего считывания с датчика
int TEMP_UPDATE_TIME = 1000; // Определяем периодичность проверок

long TimePing = 0; // Переменная для хранения времени последнего считывания с датчика
const int TimePing_UPDATE_TIME = 120000; // Определяем периодичность проверок

bool AuthSucess = false;
char AuthKey[AuthKey_Length];

byte SN[SN_Length] = {0xfc, 0xff, 0xca, 0xfa,       0x4d, 0x49, 0x43, 0x33, 0x31, 0x34, 0x38, 0x44, 0x43, 0x30, 0x35, 0x30, 0x38, 0x32, 0x32,0x00};

/*
 * Setup function. Here we do the basics
 */
void setup(void)
{
  // start serial port
  Serial.begin(115200);
  //Serial.println("Dallas Temperature Microf Control");
  sensors.begin();
  //sensors.setResolution(insideThermometer, DefaultResolution);
  printInformation();
}

int CD;

void printInformation(){
  TimePing = millis();
  Serial.println("Informations:");
  Serial.print("Found ");
  CD = sensors.getDeviceCount();
  Serial.print(CD, DEC);
  Serial.println(" devices.");

  for(int i = 0; i < CD; i++){
    sensors.getAddress(insideThermometer[i], i);        
    // report parasite power requirements
    Serial.print("Parasite power is device "+(String)i+": "); 
    if (sensors.isParasitePowerMode()) Serial.println("ON");
    else Serial.println("OFF");   

    if (CD-1 == i && !sensors.getAddress(insideThermometer[i], i)) Serial.println("Unable to find address for Device "+i);
     
    // show the addresses we found on the bus
    Serial.print("Device "+(String)i+" Address: ");
    printAddress(insideThermometer[i]);
    Serial.println();
   
    Serial.print("Device "+(String)i+" Resolution: ");
    Serial.print(sensors.getResolution(insideThermometer[i]), DEC); 
    //Serial.println(" bit");
    if(i != CD-1) Serial.println();
  }
  Serial.println(";");
}

// function to print the temperature for a device
void printTemperature(DeviceAddress deviceAddress, int i)
{
  // method 2 - faster
  float tempC = sensors.getTempC(deviceAddress);
  Serial.print("Device "+(String)i+" Temp C: ");
  #if defined(UseFilter)
    while(Filter.filtered(tempC) == 0)
      Filter.filtered(tempC);
    Serial.println(Filter.filtered(tempC));
  #else
    Serial.println(tempC);
  #endif
  Serial.println(";");
}

void printID()
{
  TimePing = millis();
  Serial.println("Identificator:");
  for(uint8_t i = 0; i < SN_Length; i++){
    //if(i == 4)
    //  for(uint8_t r = 2; r < AuthKey_Length; r++)
    //    Serial.print(AuthKey[r], HEX);  
    Serial.print(SN[i], HEX);
  }
  Serial.println();
  Serial.println("Identificator data end");
  Serial.println(";");
}

void Authorized(String key)
{
  digitalWrite(LED_BUILTIN, 1);
  key.toCharArray(AuthKey, AuthKey_Length);
  if(key[0] == 'K' && key[1] == 'T'){
    AuthSucess = true;
    printInformation();
    Serial.print("Auth OK;");
    TimePing = millis();
  }
  else
    AuthSucess = false;    
}

void DisAuthorized()
{
  digitalWrite(LED_BUILTIN, 0);
  AuthSucess = false;    
  Serial.println("DisAuthorized");
  Serial.println(";");
}

void readTemperatures(){
  for(int i = 0; i < CD; i++){
    // call sensors.requestTemperatures() to issue a global temperature 
    // request to all devices on the bus
    Serial.print("Requesting temperatures...");
    sensors.requestTemperatures(); // Send the command to get temperatures
    Serial.println("DONE");
    
    // It responds almost immediately. Let's print out the data
    printTemperature(insideThermometer[i], i); // Use a simple function to print out the data
  }
}
/*
 * Main function. It will request the tempC from the sensors and display on Serial.
 */
void loop(void)
{ 
  if(Serial.available() > 0){
    String read = Serial.readStringUntil(';');
    String type = read.substring(0,2);
    String data = read.substring(2);

    //Serial.println(read + " | " + type + " | " + data + ";");
    
    if(read == "ping") { Serial.println("PING OK;"); TimePing = millis(); }
    else if(type == "sr" && AuthSucess) for(int i = 0; i < CD; i++) sensors.setResolution(insideThermometer[i], data.toInt());
    else if(type == "pi" && AuthSucess) printInformation();
    else if(type == "ia" && AuthSucess) printID();
    else if(type == "ds" && AuthSucess) DisAuthorized();
    else if(type == "pt" && AuthSucess) readTemperatures();
    else if(type == "st" && AuthSucess) TEMP_UPDATE_TIME = data.toInt();
    else if(type == "au") Authorized(data);
  }

  if (millis() - lastUpdateTime > TEMP_UPDATE_TIME)
  {
    lastUpdateTime = millis();
    if(AuthSucess)
      readTemperatures();
    else Serial.println("No Auth;");
  }
  
  if (millis() - TimePing > TimePing_UPDATE_TIME && AuthSucess)
  {
    TimePing = millis();
    DisAuthorized();
  }
}

// function to print a device address
void printAddress(DeviceAddress deviceAddress)
{
  for (uint8_t i = 0; i < 8; i++)
  {
    if (deviceAddress[i] < 16) Serial.print("0");
    Serial.print(deviceAddress[i], HEX);
  }
}
