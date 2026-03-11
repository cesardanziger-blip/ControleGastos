import api from "../api/api";
import { RelatorioData } from "../types";

export const totaisCompletos = async (): Promise<RelatorioData> => {
  const response = await api.get("/relatorio/SaldoTotal");
  return response.data; // já vem com pessoas + categorias + totais
};

export const totaisPorPessoa = async (): Promise<RelatorioData["pessoas"]> => {
  const data = await totaisCompletos();
  return data.pessoas;
};

export const totaisPorCategoria = async (): Promise<RelatorioData["categorias"]> => {
  const data = await totaisCompletos();
  return data.categorias;
};