# Arbitro

## Metodo `CrearJuego` :
Una vez crado el juego el botodon asignadoa la creacion del juego desaparecera de la interfaz visual para garantizar la lenitud del transcurso de un posible juego actual. 
Por eso no es necesario preguntar el estado del juego. Pues nos aseguramos de que cuanto el usuario llegue a la paestaña donde aparece el boton de crear juego, este ya renuncio a cualquier otro estado posible del juego que no fue un juego nuevo.

# Cambios realizados luego de la previa revision
- 1- Se elimino de la interfaz `GuiaJuego` ya que solo existe una instancia que encapsyla el concepto Arbitro.
- 2- Se eliminaron los codigos comentados.