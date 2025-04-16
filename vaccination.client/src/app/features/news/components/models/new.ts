export interface News {
  id: string;
  content: string;
  preview: string;
  title: string;
  postDate: string;
  newsTypeId: string;
  newsType: NewsType;
}

export interface NewsType {
  id: string;
  description: string;
  newsTypeName: string;
}
