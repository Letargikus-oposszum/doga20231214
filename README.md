1. 1. feladat: <Módosító> <visszatérési érték típusa> <metódus neve> <paratéterek>{

}
2. feladat:
Console.WriteLine("Mi a hiba?");
WriteSomething(20);
Console.WriteLine(number);

void WriteSomething(int number)
{
	var number = 10;
	Console.WriteLine(number);
}
-- itt a hiba az, hogy a "number" változót a Console.WriteLine sor előtt dekleráltuk így a változó gyakorlatilag nem volt látható a Console.WriteLine-nak

3. feladat:
  First() – egy feltételnek megfelelő sorozat első elemét adja vissza.
  TakeLast() – a sorozat utolsó N elemét kéri le.
  Skip() – kihagy egy meghatározott számú elemet, és visszaadja a sorozat többi elemét.
  Chunk() – az elemek sorozatának felosztása darabokra, maximális számú elemmel.
  Contains() – igazat ad vissza, ha egy sorozat elemet tartalmaz, vagy hamis értéket egyébként.
