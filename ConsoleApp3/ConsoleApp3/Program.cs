A<B> asd = new ASd<C>();

interface A<out T> where T : B
{

}

class ASd<T> : A<T> where T : B
{

}

class B
{

}

class C : B
{

}

