# **Computação Gráfica - Relatório**
## **Shader com colisões**




#### Trabalho realizdo por:

- António Rodrigues - a22202884

#### Relatório:

Comecei por procurar o “óbvio” e pesquisar como fazer um shader que reagia a colisões, com isso encontrei dois videos:

[Shader Graph Forcefield: Update](https://www.youtube.com/watch?v=P47yMdetoE4&ab_channel=WilmerLinGASchool) - Acabei por sentir que este vídeo não fazia o que queria, usava raycasts para determinar os impactos, e não colisões, mas retirei de lá a ideia de usar um ripple effect.

[Energy Shield Effect in Unity URP Shader Graph](https://www.youtube.com/watch?v=o4CGL2YXs5k&ab_channel=Imphenzia) - Este fazia quase tudo o que queria e usei como referência para começar.

Depois disto achei que procurar por um ripple effect seria o melhor começo, pois a partir daí, caso conseguisse determinar a posição das colisões, poderia enviar essa informação para o shader, e causar os ripple effects no local da colisão.

Pesquisei então por ripple effects e encontrei este video: 

[Shockwave Shader Graph - How to make a shock wave shader in Unity URP/HDRP](https://www.youtube.com/watch?v=dFDAwT5iozo&ab_channel=GameDevBill) 

Este video foi um bom começo, e permitiu-me chegar a um resultado parecido ao efeito que queria:

![Efeito ripple em sprite](https://media.discordapp.net/attachments/1163146681064357908/1191715615648530492/image.png?ex=65a672a7&is=6593fda7&hm=b81a781def3c2e51267f32dceef996ccaa3d4d1447a411394691035881e6ec89&=&format=webp&quality=lossless&width=746&height=407)
                                                    
Demonstração do efeito: [Vídeo](https://drive.google.com/file/d/1o_DCMi-iCpm2Wfh4DWscVADBldpeMY7n/view?usp=sharing)

Mas depois disto senti que tinha pouco controlo, no primeiro que era específico para sprites senti-me me com mais controlo pois o shader permitia mudar o focal point num espaço 2D facilmente, e consegui também fazer um efeito com mais que um ripple:

![Efeito ripple em sprite](https://media.discordapp.net/attachments/1163146681064357908/1191715682824491098/image.png?ex=65a672b7&is=6593fdb7&hm=a546eec052eab20fb6a1575d3975ea9a1e95d002b209614dc0fbb630bba6631b&=&format=webp&quality=lossless&width=696&height=272)

Demonstração do efeito: [Vídeo](https://drive.google.com/file/d/1GrYGJ1sYELv5E832-VCtKjNdcqq5AHFU/view?usp=sharing)

Mas no caso da esfera, senti que tinha pouco controlo, e mesmo depois de algumas mudanças, e alterando o focal pointo para um Vector3, eu conseguia controlá-lo facilmente no eixo do X e do Y, mas estava com dificuldades no eixo do Z, garantidamente que era por não ter entendido tudo o que retirei do vídeo anterior, mas decidi voltar a pesquisar.

Com isso encontrei este vídeo:
[Unity Shader Graph VFX - Bubble Shield (Tutorial)](https://www.youtube.com/watch?v=jdAbVkre8cw&ab_channel=ABitOfGameDev)

Com este tutorial aprendi algus efeitos como o twirl, que achei que seriam interessantes para o shader final, mas este tutorial usava uma esfera com UVs alterados, e eu quero criar um shader que funcione com qualquer objeto, especialmente os default do Unity:

![Twirl](https://media.discordapp.net/attachments/1163146681064357908/1191715707428274286/image.png?ex=65a672bd&is=6593fdbd&hm=9607fa7bc45a8512d1ff2fc9c035e538c2bc5bd26930d70e9f256543a60ae93e&=&format=webp&quality=lossless&width=590&height=391)


Apesar de o material estar a mudar, a esfera em si não mudava, suspeitei que fosse  por a esfera do vídeo ter UVs alterados, não tinha maya como no vídeo mas instalei blender para confirmar a teoria, após criar a esfera com os UVs como no tutorial, a esfera continuou sem deformação:

![Sem deformação](https://media.discordapp.net/attachments/1163146681064357908/1191715738969448558/image.png?ex=65a672c5&is=6593fdc5&hm=49641b2a1b3b3a8482846a2861403279ea0a20d5d69e98554ef42e5b1e06710e&=&format=webp&quality=lossless&width=527&height=473)

Depois disto achei que já tinha material suficiente para começar o meu shader.


### **Comecei a fazer o meu shader:**

Criei um timer que me permite ir de 0 a 1 com a função sen:
![Timer Sin](https://media.discordapp.net/attachments/1163146681064357908/1191715765779435540/image.png?ex=65a672cb&is=6593fdcb&hm=fc890a77960514cf269a99bc0620b963f4d4d13364db0aaae7a7b193e04c1e0f&=&format=webp&quality=lossless&width=752&height=317)

Em vez de usar diretamente o sine time, usamos o multiply pelo meio, para podemos alterar a frequência do time, e depois fazemos o sine, assim mantemos o intervalo entre 0 e 1 mas mudamos quanto oscila por segundo.
Criei isto com o intuito de começar por ter um bubbling effect, então a oscilação constante do sen era perfeita para isso.
Depois disso criei um grupo para mudar a posição dos vértices: para isso multiplico o resultado do grupo anterior pela normal dos vértices, depois adiciono essa mudança a posição de cada vértices, que vai ser na direção perpendicular ao vértices (o vetor normal de cada vértice), criando este efeito de expansão:
![Diagrama Vertices](https://media.discordapp.net/attachments/1163146681064357908/1191715793948389427/image.png?ex=65a672d2&is=6593fdd2&hm=063bbb9536e778aec98c037a7f9e78307828328dcfaf67f170d1aaa3558ff51c&=&format=webp&quality=lossless&width=498&height=363)

(Diagrama que exemplifica a direção de cada vetor normal para cada vértice)

![Nodes Vertices](https://media.discordapp.net/attachments/1163146681064357908/1191715832888299630/image.png?ex=65a672db&is=6593fddb&hm=696f149bd3529aada1332c3084e8b9759b848c2658caec4cc67b574c009f183b&=&format=webp&quality=lossless&width=581&height=576)

Com isto consegui o efeito pretendido:

Demonstração do efeito: [Vídeo](https://drive.google.com/file/d/153m3BaKkgHv-GbG429akcZVEX9hsjYSp/view?usp=sharing)

Usando agora alguns componentes que vi serem usados num dos vídeos, criei o anel que percorre a mesh:

![Nodes for ring](https://media.discordapp.net/attachments/1163146681064357908/1191719104504143912/image.png?ex=65a675e7&is=659400e7&hm=cbdd74181728155634a64ebe56961cb281fd19a572a6055e7d3af4076e0f8f8f&=&format=webp&quality=lossless&width=1440&height=540)

Recebemos o valor para a progressão da onda, ou seja onde está o anel, e usamos o componente fraction que nos dá a parte decimal do valor, depois pegamos neste valor e adicionamos-lhe o size, e noutro node subtraímos o size a ele, um exemplo disto: caso o valor estivesse no 0.6, e o size fosse 0.1, teríamos os valores 0.7 e 0.5, tendo assim uma wave de tamanho 0.2:

![Fraction node](https://media.discordapp.net/attachments/1163146681064357908/1191719249505419346/image.png?ex=65a6760a&is=6594010a&hm=fc932fe8d051d077c5ca6ac755a71c551c5b2024e7b3c446f079f1170c0d479e&=&format=webp&quality=lossless&width=837&height=643)



Depois disso, crio uma secção à parte, esta secção recebe o ponto de impacto (FocalPoint), normaliza este vetor para obter a direção, e neste caso, como estamos a aplicar uma esfera, dividimos por 2, pois queremos o comprimento de vetor a 0.5, já que o raio da esfera seria 0.5 e o diâmetro 1, as posições da mesh estão sempre compreendidas entre -0.5 e 0.5. Depois subtraímos a direção pela posição no objeto, para termos a posição inicial da shockwave no objeto. O node da length dá-nos a distância até esta posição inicial no objeto:

![Nodes for focal point](https://media.discordapp.net/attachments/1163146681064357908/1191719421551583293/image.png?ex=65a67633&is=65940133&hm=92cedcc69fa72013445ccdcda41f7ebbbfda8c5eee2b002fd4c3015b16e6b37c&=&format=webp&quality=lossless&width=1077&height=668)

Criei um node smoothstep para receber estes inputs, ao receber o add e o subtract, obtemos os limites do nosso anel e a length dá-nos o ponto inicial a partir de qual o anel se afasta e expande ao longo da mesh:

![Smoothstep Node](https://media.discordapp.net/attachments/1163146681064357908/1191725059522441318/image.png?ex=65a67b73&is=65940673&hm=7d76d4e329af7f3825b69a8676e4e24f8ffc776277d1e37f4129d172bce63e37&=&format=webp&quality=lossless&width=593&height=668)

Dou este resultado a um One Minus node, como o smoothstep vai estar sempre compreendido entre 0 e 1, o one minus acaba por nos dar a diferença do seu input. Dando o valor oposto ao smoothstep. Quando multiplicamos os dois resultados, tudo o que está a 0 em ambos é removido no outro, e onde não houver zeros, ficamos com um gradiente, que é mais forte no centro (onde ambos os inputs têm valor mais alto):

![One minus node](https://media.discordapp.net/attachments/1163146681064357908/1191726049508200528/image.png?ex=65a67c5f&is=6594075f&hm=526614aadb30b5debd7fc4452281b78f64a4b7bf5170a332b09b0662bedb5e47&=&format=webp&quality=lossless&width=895&height=670)


Multiplicamos este valor pela nossa amplitude, o que altera o tamanho dos nossos valores que não são zero, fazendo assim com que a onda aumenta ou diminua de tamanho:

![Amplitude](https://media.discordapp.net/attachments/1163146681064357908/1191728549648924803/image.png?ex=65a67eb3&is=659409b3&hm=603ba354f507ed5fc0071dc92a2d093da28de71a62c8d0352abe7d0e841272fc&=&format=webp&quality=lossless&width=716&height=670)

Finalmente, damos este resultado ao nosso grupo de que altera a posição dos vértices e obtemos a deformação:

![Final node connection](https://media.discordapp.net/attachments/1163146681064357908/1191729117985525871/image.png?ex=65a67f3a&is=65940a3a&hm=2db841514aed86cb5f361095a4b9a88102701dbbdf69f53e229d8e8cf2d4a7c5&=&format=webp&quality=lossless&width=888&height=670)

Demonstração do efeito: [Vídeo](https://drive.google.com/file/d/1PPWpBOCNbuVdiQQhesbG8JtEANb8bVj7/view?usp=sharing)

