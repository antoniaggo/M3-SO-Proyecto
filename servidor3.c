#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <ctype.h>
#include <mysql.h>
#include <pthread.h>

#define max_users 50

#define port 50007

#define database_host "localhost"
#define database_name "M3_game"
#define database_username "root"
#define database_password "mysql"

typedef struct
{
	int socket; // User's socket
	pthread_t thread; //User's thread
	int logged_in;
	int partida;
	int accepted;
	char nombre[20];
	char username[20]; //Username
}User;

typedef struct
{
	int num;
	User users[max_users];

}UserList; 

UserList user_list;
MYSQL *conn;
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;

int get_user_socket(int socket);
void *AtenderCliente (void *socket);
int main(int argc, char *argv[]);
void delete_user(int socket);
void send_client(char buffer[512], int socket, char who[50]);
void writeint(char buffer[512], int num);
void send_user_list(char output[512]);

void send_user_list(char output[512]) {

	writeint(output, user_list.num);

	for (int i = 0; i < user_list.num; i++) {
		
		if (user_list.users[i].logged_in == 1) { 

			sprintf(output, "%s%s/", output, user_list.users[i].username);
		
		}
		
	}

}

void writeint(char buffer[512], int num) {

	char n[10];
	sprintf(n, "%d", num);
	strcat(buffer, n);
	strcat(buffer, "/");

}

void delete_user(int socket) {

	pthread_mutex_lock(&mutex);

	int i = get_user_socket(socket);

	for (int a = i; a < (user_list.num - 1); a++){

		user_list.users[a] = user_list.users[a + 1];

	}

	user_list.num--;

	pthread_mutex_unlock(&mutex);

	char buffer[512];
	writeint(buffer, 6);
	send_user_list(buffer);
	send_client(buffer, socket, "all");

}

void send_client(char buffer[512], int socket, char who[50]) {

	buffer[strlen(buffer)] = '\0';

	if (strcmp(who, "self") == 0) {
	
		write(socket, buffer, strlen(buffer)); 

	} else if (strcmp(who, "others") == 0) {

		for (int i = 0; i < user_list.num; i++)
			if (user_list.users[i].socket != socket)
				if (user_list.users[i].logged_in)
					write(user_list.users[i].socket, buffer, strlen(buffer)); 

	} else if (strcmp(who, "all") == 0) {

		for (int i = 0; i < user_list.num; i++)
			if (user_list.users[i].logged_in)
				write(user_list.users[i].socket, buffer, strlen(buffer)); 

	} else {

		printf("send_client error\n");

	}

}
	

int get_user_socket(int socket) {

	for (int pos = 0; pos < user_list.num; pos++)
		if (user_list.users[pos].socket == socket)
			return pos;

}

void *AtenderCliente (void *socket)
{
	int sock_conn = *((int *) socket);		
	// Entramos en un bucle para atender todas las peticiones de este cliente
	//hasta que se desconecte
	int terminar = 0;
	
				
	while (terminar == 0)
	{
	
		char respuesta[512];
		char peticion[512];

		char *p;
		
		char consulta[512];
		char buff2[512];
		
		char nombre[512];
		char partida[512];
		char usuario[512];
		char contrasenya[512];
		char conectados[100];
		char notificacion[20];
		
		int err;
		int numero=0;
		int IDUs;

		// Ahora recibimos la peticion
		if (read(sock_conn, peticion, sizeof(peticion)) <= 0) {
			
			puts("Client disconnected");
			break; 
			
		}
		
		printf ("Peticion: %s\n", peticion);

		
		MYSQL_RES *resultado;
		MYSQL_ROW row;

		
		switch (atoi(strtok(peticion, "/"))) { 
		
			case 0:
			    puts("Client disconnected");
				terminar = 1;
			break;
			
			case 1:
				
				
				p = strtok( NULL, "/");
					
				strcpy (nombre, p);
					
				p= strtok(NULL, "/" );
					
				strcpy(contrasenya,p);
				
				sprintf(consulta,"SELECT Name FROM player WHERE Name='%s' AND Pass='%s'",nombre,contrasenya);
				// hacemos la consulta 
				err=mysql_query(conn, consulta); 
				if (err!=0) 
				{
					printf ("Error al consultar datos de la base %u %s\n",
							mysql_errno(conn), mysql_error(conn));
					 
				}
				resultado = mysql_store_result (conn); //si lo encuentra en la base de datos sigue por ese camino
				numero=mysql_num_rows(resultado);
				row = mysql_fetch_row (resultado);
				strcpy(respuesta,"");
				if (row == NULL)
				{
					
					
					
					strcat(respuesta,"1/No se ha encontrado el usuario");
					printf ("No se ha encontrado el usuario\n");
					write (sock_conn,respuesta, strlen(respuesta));
					
					
				}
				else
				{

					pthread_mutex_lock(&mutex);
					for (int i = 0; i < user_list.num; i++) {
		
						if (user_list.users[i].socket == sock_conn) { 

                            user_list.users[i].logged_in = 1;
							strcpy(user_list.users[i].username, row[0]);
		
						}
		
					}
					pthread_mutex_unlock(&mutex);
					
					char buffer[512];
					writeint(buffer, 6);
					send_user_list(buffer);
					send_client(buffer, sock_conn, "all");
		
					printf ("Usuario %s encontrado.\n", row[0] );
					strcat(respuesta,"1/Usuario encontrado");
					write (sock_conn,respuesta, strlen(respuesta));
				}
			
			break;
			
			case 2:
			
				p = strtok( NULL, "/");
					
				strcpy (nombre, p);
					
				p= strtok(NULL, "/" );
					
				strcpy(contrasenya,p);
			
				// construimos la consulta SQL
				
				//buscamos el ultimo ID asignado a un usuario
				strcpy (consulta,"SELECT MAX(player.Identificador) FROM player");
				err=mysql_query (conn, consulta); 
				if (err!=0) {
					printf ("Error al consultar datos de la base %u %s\n",
							mysql_errno(conn), mysql_error(conn));
					strcpy (buff2,"2/1/0,");
		/*					strcat(respuesta,"Usuario encontrado");*/
					write (sock_conn,respuesta, strlen(respuesta));
					
				}
				else{
					//recogemos el resultado de la consulta resultado = mysql_store_result (conn); 
					resultado = mysql_store_result (conn);
					numero=mysql_num_rows(resultado);
					row = mysql_fetch_row (resultado);
					if (row == NULL)
					{
						printf ("No se ha obtenido ningun ID\n");
						IDUs=0;
						strcat(respuesta,"2/Usuario incorrectamente registrado");
						printf ("%s",respuesta);
						write (sock_conn,respuesta, strlen(respuesta));
					}
					else
					{
						IDUs=atoi(row[0])+1;
						//Le sumamos 1 al ultimo ID asignado para conseguir el del nuevo usuario
						strcat(respuesta,"2/Usuario correctamente registrado ");
						write (sock_conn,respuesta, strlen(respuesta));
					}
				}
				sprintf (consulta,"INSERT INTO player(Identificador,Name,Pass,Wingame,Losegame) VALUES ('%d','%s','%s',0,0)",IDUs,nombre,contrasenya);
				// hacemos la consulta 
				err=mysql_query (conn, consulta); 
				if (err!=0) {
					printf ("Error al consultar datos de la base %u %s\n",
							mysql_errno(conn), mysql_error(conn));
					strcpy (respuesta,"1/1,");
					
				}
				else
				{
					strcpy (respuesta,"1/0,");
					
				}
				
			break;
			
			case 3:
				resultado=mysql_store_result(conn);
				
				err=mysql_query (conn, "select relations.points from relations where relations.points=(select max(relations.points) from relations)");
				if (err!=0)
				{
					printf ("Error al consultar datos de la base %u %s\n",
							mysql_errno(conn), mysql_error(conn));
					exit (1);
				}
				
				resultado = mysql_store_result (conn);
				
				row= mysql_fetch_row(resultado);
				
				if (row== NULL)
				{
					printf("No se han obtenido datos en la consulta\n");
					strcat(respuesta,"3/Usuario incorrectamente registrado");
					write (sock_conn,respuesta, strlen(respuesta));
				}
				else
				{
					printf("NOmbre de la persona ganadora: %s\n", row[0]);
					sprintf(respuesta,"3/Puntuacion maxima: %s",row[0]);
					write (sock_conn,respuesta, strlen(respuesta));
				}
			break;
			
			case 4:
			
				strcpy(respuesta,"4/");
				strcpy(consulta,"SELECT * FROM player"); //Seleciona todos los jugadores de mi lista
				err=mysql_query (conn, consulta); 
				if (err!=0) {
					printf ("Error al consultar datos de la base %u %s\n",
							mysql_errno(conn), mysql_error(conn));
					exit (1);
				}
				//recogemos el resultado de la consulta 
				resultado = mysql_store_result (conn); 
				row = mysql_fetch_row (resultado);
				if (row == NULL)
					printf ("No se han obtenido datos en la consulta\n");
				else
				{
					while (row !=NULL) 
					{
						
						printf ("ID: %s, Username: %s, Password: %s\n", row[0], row[1], row[2]);
		/*						sprintf(respuesta,"Resultado %s /n", row[0]);*/
		/*						write (sock_conn,respuesta, strlen(respuesta));	*/
		
						sprintf(respuesta,"%sResultado %s pass: %s \n", respuesta, row[1], row[2]);
						
						row = mysql_fetch_row (resultado);
					}
					
					puts(respuesta);
					
					write (sock_conn, respuesta, strlen(respuesta));
					
				}
				
			break;
			
			case 5:
			
				p = strtok( NULL, "/");
				
				strcpy (nombre, p);
				// Ya tenemos el nombre
				printf ("Codigo: %d, Nombre: %s\n", 5, nombre);
				strcpy (consulta,"SELECT player.Pass FROM player WHERE player.Name='");
				strcat(consulta, nombre);
				/*	strcat(consulta, "'");*/
				strcat(consulta, "'");
				// hacemos la consulta 
				err=mysql_query (conn, consulta); 
				if (err!=0) {
					printf ("Error al consultar datos de la base %u %s\n",
							mysql_errno(conn), mysql_error(conn));
					exit (1);
				}
				//recogemos el resultado de la consulta 
				resultado = mysql_store_result (conn); 
				row = mysql_fetch_row (resultado);
				if (row == NULL)
				{
					printf ("No se han obtenido datos en la consulta\n");
					strcpy (respuesta, "5/No existe");
					write (sock_conn,respuesta, strlen(respuesta));	
				}
				else
				{
					while (row !=NULL)
					{ 
						
						
						printf ("Password: %s\n", row[0] );
						strcpy(respuesta, row[0]);
						row = mysql_fetch_row (resultado);
						write (sock_conn,respuesta, strlen(respuesta));
				
					}
						
				}
			
			break;
			
			case 10:
				{
				char buffer[512];
				writeint(buffer, 10);
				send_client(buffer, sock_conn, "all");
			}
			break;
			
			case 11:
			
				pthread_mutex_lock(&mutex);
				for (int i = 0; i < user_list.num; i++) {
	
					if (user_list.users[i].socket == sock_conn) { 

						user_list.users[i].accepted = 1;

					}
	
				}
				pthread_mutex_unlock(&mutex);
				
				int en = 0;
				
				for (int i = 0; i < user_list.num; i++) {
	
					if (user_list.users[i].accepted == 0) { 

						en = 1;

					}
	
				}
				
				if (en == 0){ 
					
					char buffer[512];
					writeint(buffer, 11);
					send_client(buffer, sock_conn, "all");
		
				}
				
			break;
			
			case 12:
			{
			    int part = 0;
			
				for (int i = 0; i < user_list.num; i++) {
	
					if (user_list.users[i].socket == sock_conn) { 

						part = user_list.users[i].partida;
						break;
	
					}
	
				}
				
				pthread_mutex_lock(&mutex);
				for (int i = 0; i < user_list.num; i++) {
	
					if (user_list.users[i].partida == part) { 

						user_list.users[i].partida = 0;
						user_list.users[i].accepted = 0;
	
					}
	
				}				
				pthread_mutex_unlock(&mutex);
				}	
			break;
			
		}
		
	}
 
	
	// Se acabo el servicio para este cliente
	close(sock_conn);
	
	delete_user(sock_conn);
	
	puts("Connection closed");
	
}

	
int main(int argc, char *argv[])
{

	int sock_listen;
	struct sockaddr_in serv_adr;
	
	user_list.num = 0;
	
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0) {
		
		perror("Error while creating listen socket");
		exit(1);
		
	}
	
	int reuse = 1; 
	if (setsockopt(sock_listen, SOL_SOCKET, SO_REUSEADDR, &reuse, sizeof(int)) < 0)
		perror("Failed to reuse address");
		
	memset(&serv_adr, 0, sizeof(serv_adr));
	serv_adr.sin_family = AF_INET;
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	serv_adr.sin_port = htons(port);
	
	if (bind(sock_listen, (struct sockaddr*)&serv_adr, sizeof(serv_adr)) < 0) {
		
		perror("Bind error");
		exit(1);
		
	}
	
	if (listen(sock_listen, 10) < 0) {
		
		perror("Listen error");
		exit(1);
		
	}
	
	
	conn = mysql_init(NULL);
	
	if (conn == NULL) {
		
		printf("Error initializing MySQL: %u %s\n", mysql_errno(conn), mysql_error(conn));
		exit(1);
		
	}
	
	conn = mysql_real_connect(conn, database_host, database_username, database_password, database_name,0, NULL, 0 );
	
	if (conn == NULL) {
		
		printf("Error connecting to database: %u %s\n", mysql_errno(conn), mysql_error(conn));
		exit(1);
		
	}

	while (1) { 
		
		printf ("Escuchando\n");
		
		int s = accept(sock_listen, NULL, NULL);
		printf ("He recibido conexion\n");


		pthread_mutex_lock(&mutex);
		user_list.users[user_list.num].socket = s;
		user_list.users[user_list.num].logged_in = 0;
		user_list.users[user_list.num].partida = 0;
		user_list.users[user_list.num].accepted = 0;
		user_list.num++;
		pthread_mutex_unlock(&mutex);
		
		pthread_create(&user_list.users[user_list.num - 1].thread, NULL, AtenderCliente, &s);
	
	}
		
	mysql_close(conn);
	
}
