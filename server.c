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
#include <strings.h>

#define max_users 50

#define port 9002  //50007

#define database_host "localhost"
#define database_name "game" //M3_game
#define database_username "root"
#define database_password "mysql"

typedef struct
{
	int socket; // User's socket
	pthread_t thread; //User's thread
	int logged_in;
	int partida;
	int accepted;
	int playing;
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

int partidas;

int get_user_socket(int socket);
void *AtenderCliente (void *socket);
int main(int argc, char *argv[]);
void delete_user(int socket);
void send_client(char buffer[512], int socket, char who[50]);
void writeint(char buffer[512], int num);
void send_user_list(char output[512]);

void send_user_list(char output[512]) {
	
	int a = 0;
	
	for (int i = 0; i < user_list.num; i++) {
		
		if (user_list.users[i].logged_in == 1) { 

			a++;
		}
		
	}

	writeint(output, a);

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

		puts("send_client error");

	}

}
	

int get_user_socket(int socket) {

	for (int pos = 0; pos < user_list.num; pos++)
		if (user_list.users[pos].socket == socket)
			return pos;

}

int get_user_username(char username[512]) {

	for (int pos = 0; pos < user_list.num; pos++)
		if (strcmp(user_list.users[pos].username, username) == 0)
			return pos;

}

void *AtenderCliente (void *socket)
{
	int sock_conn = *((int *) socket);		

	while (1)
	{
	
		char buffer[512];
		bzero(buffer, 512);

		char *p;
		
		char consulta[512];
		bzero(consulta, 512);
		
		char nombre[512];
		bzero(nombre, 512);
		
		char contrasenya[512];
		bzero(contrasenya, 512);

		// Ahora recibimos la peticion
		if (read(sock_conn, buffer, sizeof(buffer)) <= 0) {
			
			puts("Client disconnected");
			break; 
			
		}
		
		printf ("Peticion: %s\n", buffer);

		
		MYSQL_RES *resultado;
		MYSQL_ROW row;

		
		switch (atoi(strtok(buffer, "/"))) { 
			
			case 1:
				
				p = strtok( NULL, "/");
					
				strcpy (nombre, p);
					
				p = strtok(NULL, "/" );
					
				strcpy(contrasenya,p);
				
				sprintf(consulta,"SELECT Name FROM player WHERE Name='%s' AND Pass='%s'",nombre,contrasenya);

				if (mysql_query(conn, consulta) != 0) 
				{
					
					printf ("Error al consultar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
					 
				}
				
				resultado = mysql_store_result(conn); //si lo encuentra en la base de datos sigue por ese camino
				row = mysql_fetch_row(resultado);
				
				if (row == NULL)
				{
					
					bzero(buffer, 512);
					strcpy(buffer, "1/0/");
					send_client(buffer, sock_conn, "self");
					
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
					
					bzero(buffer, 512);
					writeint(buffer, 6);
					send_user_list(buffer);
					send_client(buffer, sock_conn, "all");
		
					bzero(buffer, 512);
					strcpy(buffer,"1/1/");
					send_client(buffer, sock_conn, "self");
					
				}
			
			break;
			
			case 2:
			
			
				p = strtok( NULL, "/");	
				strcpy(nombre, p);
					
				p = strtok(NULL, "/");
				strcpy(contrasenya, p);

				strcpy (consulta, "SELECT MAX(player.Identificador) FROM player");

				int IDUs;
				IDUs = 0;
								
				if (mysql_query (conn, consulta) == 0) 
				{

					resultado = mysql_store_result (conn);

					row = mysql_fetch_row (resultado);
					
					if (row != NULL)
					{
						
						IDUs = atoi(row[0]) + 1;
						
					}
					
				}
				
				sprintf (consulta,"INSERT INTO player(Identificador,Name,Pass,Wingame,Losegame) VALUES ('%d','%s','%s',0,0)", IDUs, nombre, contrasenya);

				if (mysql_query (conn, consulta) != 0) {
					
					bzero(buffer, 512);
					strcpy(buffer,"2/0/");
					send_client(buffer, sock_conn, "self");
					
				}
				else
				{
					
					bzero(buffer, 512);
					strcpy(buffer,"2/1/");
					send_client(buffer, sock_conn, "self");
					
				}
				
			break;
			
			case 3:
				resultado = mysql_store_result(conn);
			
				if (mysql_query(conn, "select relations.points from relations where relations.points=(select max(relations.points) from relations)") != 0)
				{
					
					printf ("Error al consultar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
					exit (1);
					
				}
				
				resultado = mysql_store_result (conn);
				
				row = mysql_fetch_row(resultado);
				
				if (row == NULL)
				{
					
					printf("No se han obtenido datos en la consulta\n");
					strcat(buffer,"3/Usuario incorrectamente registrado");
					write (sock_conn,buffer, strlen(buffer));
					
				}
				else
				{
					
					printf("NOmbre de la persona ganadora: %s\n", row[0]);
					sprintf(buffer,"3/Puntuacion maxima: %s",row[0]);
					write (sock_conn,buffer, strlen(buffer));
					
				}
				
			break;
			
			case 4:
			
				strcpy(buffer,"4/");
				strcpy(consulta,"SELECT * FROM player"); //Seleciona todos los jugadores de mi lista
				
				if (mysql_query (conn, consulta) != 0) 
				{
					
					printf ("Error al consultar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
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
		
						sprintf(buffer,"%sResultado %s pass: %s \n", buffer, row[1], row[2]);
						
						row = mysql_fetch_row (resultado);
					}
					
					puts(buffer);
					
					write (sock_conn, buffer, strlen(buffer));
					
				}
				
			break;
			
			case 5:
			
				p = strtok( NULL, "/");
				
				strcpy (nombre, p);
				
				printf ("Codigo: %d, Nombre: %s\n", 5, nombre);
				strcpy (consulta,"SELECT player.Pass FROM player WHERE player.Name='");
				strcat(consulta, nombre);
				
				strcat(consulta, "'");


				if (mysql_query (conn, consulta) != 0) {
					
					printf ("Error al consultar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
					exit (1);
					
				}

				resultado = mysql_store_result (conn); 
				row = mysql_fetch_row (resultado);
				
				if (row == NULL)
				{
					
					printf ("No se han obtenido datos en la consulta\n");
					
					bzero(buffer, 512);
					strcpy (buffer, "5/0/");
					send_client(buffer, sock_conn, "self");	
					
				}
				else
				{
						
					printf ("Password: %s\n", row[0]);
					
					bzero(buffer, 512);
					strcpy(buffer, "5/1/");
					strcat(buffer, row[0]);
					sprintf(buffer, "%s/", buffer);
					send_client(buffer, sock_conn, "self");
						
				}
			
			break;
			
			case 10:
					
				pthread_mutex_lock(&mutex);
						
				p = strtok(NULL, "/");
				
				int n = atoi(p);

				int ids[500];
				
				for(int a = 0; a < n; a++) {
					
					p = strtok(NULL, "/");
					ids[a] = get_user_username(p);
					
				}

				
				for(int i = 0; i < n; i++) 
				{
				
					user_list.users[ids[i]].partida = partidas + 1;
					
					if (get_user_socket(sock_conn) != ids[i]) { 
						
						user_list.users[ids[i]].accepted = 0;
					
						bzero(buffer, 512);
						writeint(buffer, 10);
						write(user_list.users[ids[i]].socket, buffer, strlen(buffer));
					
					} else {
						
						user_list.users[get_user_socket(sock_conn)].accepted = 1;
						
					}
					
				}
				
				partidas++;
				
				pthread_mutex_unlock(&mutex);

			break;
			
			case 11:
				
				user_list.users[get_user_socket(sock_conn)].accepted = 1;
				
				int check = 0;
			
				for(int i = 0; i < user_list.num; i++) 
				{
					if (user_list.users[get_user_socket(sock_conn)].partida == user_list.users[i].partida) {
						
						if (user_list.users[i].accepted == 0) {
							
							check = 1;
							
						}
						
					}
				
				}			
				
				if (check == 0) {
					
					for(int i = 0; i < user_list.num; i++) 
					{
						if (user_list.users[get_user_socket(sock_conn)].partida == user_list.users[i].partida) {
											
							user_list.users[i].playing = 1;
							
							bzero(buffer, 512);
							writeint(buffer, 11);
							write(user_list.users[i].socket, buffer, strlen(buffer));
							
						}
					
					}	
						
				}

				
			break;
			
			case 12:

				user_list.users[get_user_socket(sock_conn)].accepted = 0;
				
				if (check == 0) {
					
					for(int i = 0; i < user_list.num; i++) 
					{
						if (user_list.users[get_user_socket(sock_conn)].partida == user_list.users[i].partida) {
							
							if (get_user_socket(sock_conn) != i) {
			
								bzero(buffer, 512);
								writeint(buffer, 12);
								write(user_list.users[i].socket, buffer, strlen(buffer));
								
							}
							
						}
					
					}	
						
				}
			    
			break;
			
			case 13:
			
				if (user_list.users[get_user_socket(sock_conn)].playing == 1) 
				{
			
					bzero(buffer, 512);
					sprintf(buffer, "13/%d/", rand() % 40);
					send_client(buffer, sock_conn, "self");
					
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
	
	partidas = 0;
	
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
		user_list.users[user_list.num].playing = 0;
		user_list.num++;
		pthread_mutex_unlock(&mutex);
		
		pthread_create(&user_list.users[user_list.num - 1].thread, NULL, AtenderCliente, &s);
	
	}
		
	mysql_close(conn);
	
}
