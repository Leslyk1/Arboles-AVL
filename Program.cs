using System;

class NodoAVL
{
    public int Valor;
    public NodoAVL Izquierdo;
    public NodoAVL Derecho;
    public int Altura;

    public NodoAVL(int valor)
    {
        Valor = valor;
        Altura = 1;
    }
}

class ArbolAVL
{
    private NodoAVL raiz;

    private int Altura(NodoAVL nodo)
    {
        return nodo == null ? 0 : nodo.Altura;
    }

    private int ObtenerBalance(NodoAVL nodo)
    {
        return nodo == null ? 0 : Altura(nodo.Izquierdo) - Altura(nodo.Derecho);
    }

    private int Maximo(int a, int b)
    {
        return (a > b) ? a : b;
    }

    private NodoAVL RotacionDerecha(NodoAVL y)
    {
        NodoAVL x = y.Izquierdo;
        NodoAVL T2 = x.Derecho;

        x.Derecho = y;
        y.Izquierdo = T2;

        y.Altura = Maximo(Altura(y.Izquierdo), Altura(y.Derecho)) + 1;
        x.Altura = Maximo(Altura(x.Izquierdo), Altura(x.Derecho)) + 1;

        return x;
    }

    private NodoAVL RotacionIzquierda(NodoAVL x)
    {
        NodoAVL y = x.Derecho;
        NodoAVL T2 = y.Izquierdo;

        y.Izquierdo = x;
        x.Derecho = T2;

        x.Altura = Maximo(Altura(x.Izquierdo), Altura(x.Derecho)) + 1;
        y.Altura = Maximo(Altura(y.Izquierdo), Altura(y.Derecho)) + 1;

        return y;
    }

    private NodoAVL Insertar(NodoAVL nodo, int valor)
    {
        if (nodo == null)
            return new NodoAVL(valor);

        if (valor < nodo.Valor)
            nodo.Izquierdo = Insertar(nodo.Izquierdo, valor);
        else if (valor > nodo.Valor)
            nodo.Derecho = Insertar(nodo.Derecho, valor);
        else
            return nodo;

        nodo.Altura = 1 + Maximo(Altura(nodo.Izquierdo), Altura(nodo.Derecho));

        int balance = ObtenerBalance(nodo);

        // Caso LL
        if (balance > 1 && valor < nodo.Izquierdo.Valor)
            return RotacionDerecha(nodo);

        // Caso RR
        if (balance < -1 && valor > nodo.Derecho.Valor)
            return RotacionIzquierda(nodo);

        // Caso LR
        if (balance > 1 && valor > nodo.Izquierdo.Valor)
        {
            nodo.Izquierdo = RotacionIzquierda(nodo.Izquierdo);
            return RotacionDerecha(nodo);
        }

        // Caso RL
        if (balance < -1 && valor < nodo.Derecho.Valor)
        {
            nodo.Derecho = RotacionDerecha(nodo.Derecho);
            return RotacionIzquierda(nodo);
        }

        return nodo;
    }

    public void Insertar(int valor)
    {
        raiz = Insertar(raiz, valor);
    }

    public void PreOrden()
    {
        PreOrden(raiz);
        Console.WriteLine();
    }

    private void PreOrden(NodoAVL nodo)
    {
        if (nodo != null)
        {
            Console.Write(nodo.Valor + " ");
            PreOrden(nodo.Izquierdo);
            PreOrden(nodo.Derecho);
        }
    }

    public void InOrden()
    {
        InOrden(raiz);
        Console.WriteLine();
    }

    private void InOrden(NodoAVL nodo)
    {
        if (nodo != null)
        {
            InOrden(nodo.Izquierdo);
            Console.Write(nodo.Valor + " ");
            InOrden(nodo.Derecho);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        ArbolAVL arbol = new ArbolAVL();
        int cantidad;
        string entrada;

        Console.WriteLine("ARBOL AVL ");

        do
        {
            Console.Write("¿Cuántos valores desea insertar?: ");
            entrada = Console.ReadLine();
        }
        while (!int.TryParse(entrada, out cantidad) || cantidad <= 0);

        for (int i = 1; i <= cantidad; i++)
        {
            int valor;
            string dato;

            do
            {
                Console.Write("Ingrese el valor #" + i + ": ");
                dato = Console.ReadLine();
            }
            while (!int.TryParse(dato, out valor));

            arbol.Insertar(valor);
        }

        Console.WriteLine();
        Console.WriteLine("Recorrido en preorden del árbol AVL:");
        arbol.PreOrden();

        Console.WriteLine("Recorrido en inorden del árbol AVL:");
        arbol.InOrden();

        Console.WriteLine("Programa finalizado.");
    }
}