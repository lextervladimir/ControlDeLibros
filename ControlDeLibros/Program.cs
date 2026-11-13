using System;
using System.Collections.Generic;

// Control de Libros de una Biblioteca Escolar

// ---------- DATOS (listas paralelas: la posicion i de cada lista es el mismo libro) ----------
List<int> libroIds = new List<int>();
List<string> libroTitulos = new List<string>();
List<int> libroAutor = new List<int>();        
List<int> libroCategoria = new List<int>();    
List<bool> libroDisponible = new List<bool>();

List<string> autores = new List<string>();
List<string> categorias = new List<string>();

int siguienteId = 1;

// ---------- MENU PRINCIPAL ----------
int opcion = -1;
while (opcion != 0)
{
    Console.WriteLine("====== Hola soy Lexter Vladimir, Bienvenidos a mi programa ======");
    Console.WriteLine("===== BIBLIOTECA ESCOLAR =====");
    Console.WriteLine("1. Registrar autor");
    Console.WriteLine("2. Registrar categoria");
    Console.WriteLine("3. Agregar libro");
    Console.WriteLine("4. Ver todos los libros");
    Console.WriteLine("5. Buscar libro");
    Console.WriteLine("6. Modificar libro");
    Console.WriteLine("7. Eliminar libro");
    Console.WriteLine("0. Salir");

    opcion = LeerEntero("Elige una opcion: ");

    if (opcion == 1) RegistrarAutor();
    else if (opcion == 2) RegistrarCategoria();
    else if (opcion == 3) AgregarLibro();
    else if (opcion == 4) MostrarLibros();
    else if (opcion == 5) BuscarLibro();
    else if (opcion == 6) ModificarLibro();
    else if (opcion == 7) EliminarLibro();
    else if (opcion == 0) Console.WriteLine("Hasta luego!");
    else Console.WriteLine("Opcion no valida.");
}

// ---------- METODOS ----------

void RegistrarAutor()
{
    Console.Write("Nombre del autor: ");
    string nombre = Console.ReadLine().Trim();

    if (nombre == "")
    {
        Console.WriteLine("El nombre no puede estar vacio.");
        return;
    }

    autores.Add(nombre);
    Console.WriteLine("Autor registrado.");
}

void RegistrarCategoria()
{
    Console.Write("Nombre de la categoria: ");
    string nombre = Console.ReadLine().Trim();

    if (nombre == "")
    {
        Console.WriteLine("El nombre no puede estar vacio.");
        return;
    }

    categorias.Add(nombre);
    Console.WriteLine("Categoria registrada.");
}

void AgregarLibro()
{
    if (autores.Count == 0 || categorias.Count == 0)
    {
        Console.WriteLine("Primero debes registrar al menos un autor y una categoria.");
        return;
    }

    Console.Write("Titulo del libro: ");
    string titulo = Console.ReadLine().Trim();

    if (titulo == "")
    {
        Console.WriteLine("El titulo no puede estar vacio.");
        return;
    }

    int autor = ElegirAutor();
    int categoria = ElegirCategoria();

    libroIds.Add(siguienteId);
    libroTitulos.Add(titulo);
    libroAutor.Add(autor);
    libroCategoria.Add(categoria);
    libroDisponible.Add(true);

    Console.WriteLine("Libro agregado con ID " + siguienteId);
    siguienteId++;
}

void MostrarLibros()
{
    if (libroIds.Count == 0)
    {
        Console.WriteLine("No hay libros registrados.");
        return;
    }

    Console.WriteLine();
    for (int i = 0; i < libroIds.Count; i++)
    {
        MostrarUnLibro(i);
    }
}

void MostrarUnLibro(int i)
{
    string estado = libroDisponible[i] ? "Disponible" : "Prestado";

    Console.WriteLine("ID: " + libroIds[i]
        + " | Titulo: " + libroTitulos[i]
        + " | Autor: " + autores[libroAutor[i]]
        + " | Categoria: " + categorias[libroCategoria[i]]
        + " | " + estado);
}

void BuscarLibro()
{
    Console.WriteLine("Buscar por: 1. Titulo  2. Autor  3. Categoria  4. Solo disponibles");
    int tipo = LeerEntero("Opcion: ");

    string texto = "";
    if (tipo >= 1 && tipo <= 3)
    {
        Console.Write("Texto a buscar: ");
        texto = Console.ReadLine().Trim().ToLower();
    }
    else if (tipo != 4)
    {
        Console.WriteLine("Opcion no valida.");
        return;
    }

    bool encontrado = false;
    Console.WriteLine();

    for (int i = 0; i < libroIds.Count; i++)
    {
        bool coincide = false;

        if (tipo == 1 && libroTitulos[i].ToLower().Contains(texto)) coincide = true;
        if (tipo == 2 && autores[libroAutor[i]].ToLower().Contains(texto)) coincide = true;
        if (tipo == 3 && categorias[libroCategoria[i]].ToLower().Contains(texto)) coincide = true;
        if (tipo == 4 && libroDisponible[i]) coincide = true;

        if (coincide)
        {
            MostrarUnLibro(i);
            encontrado = true;
        }
    }

    if (!encontrado)
    {
        Console.WriteLine("No se encontraron libros.");
    }
}

void ModificarLibro()
{
    int id = LeerEntero("ID del libro a modificar: ");
    int pos = libroIds.IndexOf(id);

    if (pos == -1)
    {
        Console.WriteLine("No existe un libro con ese ID.");
        return;
    }

    Console.WriteLine("Libro actual:");
    MostrarUnLibro(pos);

    Console.Write("Nuevo titulo (Enter para dejarlo igual): ");
    string nuevoTitulo = Console.ReadLine().Trim();
    if (nuevoTitulo != "")
    {
        libroTitulos[pos] = nuevoTitulo;
    }

    Console.Write("Cambiar autor? (s/n): ");
    if (Console.ReadLine().Trim().ToLower() == "s")
    {
        libroAutor[pos] = ElegirAutor();
    }

    Console.Write("Cambiar categoria? (s/n): ");
    if (Console.ReadLine().Trim().ToLower() == "s")
    {
        libroCategoria[pos] = ElegirCategoria();
    }

    Console.Write("Esta disponible? (s/n): ");
    libroDisponible[pos] = Console.ReadLine().Trim().ToLower() == "s";

    Console.WriteLine("Libro modificado.");
}

void EliminarLibro()
{
    int id = LeerEntero("ID del libro a eliminar: ");
    int pos = libroIds.IndexOf(id);

    if (pos == -1)
    {
        Console.WriteLine("No existe un libro con ese ID.");
        return;
    }

    MostrarUnLibro(pos);
    Console.Write("Seguro que deseas eliminarlo? (s/n): ");

    if (Console.ReadLine().Trim().ToLower() == "s")
    {
        libroIds.RemoveAt(pos);
        libroTitulos.RemoveAt(pos);
        libroAutor.RemoveAt(pos);
        libroCategoria.RemoveAt(pos);
        libroDisponible.RemoveAt(pos);
        Console.WriteLine("Libro eliminado.");
    }
    else
    {
        Console.WriteLine("Operacion cancelada.");
    }
}

// Muestra los autores y devuelve la posicion del elegido
int ElegirAutor()
{
    Console.WriteLine("Autores:");
    for (int i = 0; i < autores.Count; i++)
    {
        Console.WriteLine((i + 1) + ". " + autores[i]);
    }

    int numero = 0;
    while (numero < 1 || numero > autores.Count)
    {
        numero = LeerEntero("Numero de autor: ");
    }
    return numero - 1;
}

// Muestra las categorias y devuelve la posicion de la elegida
int ElegirCategoria()
{
    Console.WriteLine("Categorias:");
    for (int i = 0; i < categorias.Count; i++)
    {
        Console.WriteLine((i + 1) + ". " + categorias[i]);
    }

    int numero = 0;
    while (numero < 1 || numero > categorias.Count)
    {
        numero = LeerEntero("Numero de categoria: ");
    }
    return numero - 1;
}

// Pide un numero y repite hasta que el usuario escriba uno valido
int LeerEntero(string mensaje)
{
    int numero;
    Console.Write(mensaje);
    while (!int.TryParse(Console.ReadLine(), out numero))
    {
        Console.Write("Eso no es un numero. " + mensaje);
    }
    return numero;
}