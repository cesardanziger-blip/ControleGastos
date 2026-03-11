import api from "../api/api";
import { Transacao } from "../types";

export const listarTransacoes = async (): Promise<Transacao[]> => {
  const response = await api.get("/transacoes");
  return response.data;
};

export const criarTransacao = async (transacao: Omit<Transacao, "id">): Promise<Transacao> => {
  const payload = {
    descricao: transacao.descricao,
    valor: transacao.valor,
    tipo: transacao.tipo,
    PessoaId: transacao.pessoa,
    CategoriaId: transacao.categoria
  };

  const response = await api.post("/transacoes", payload);
  return response.data;
};

export const deletarTransacao = async (id: string): Promise<void> => {
  await api.delete(`/transacoes/${id}`);
};