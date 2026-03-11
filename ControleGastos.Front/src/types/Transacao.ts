export interface Transacao {
  id: string;
  descricao: string;
  valor: number;
  tipo: 1 | 2; // 1 = Despesa, 2 = Receita
  pessoa: string;
  categoria: string;
}