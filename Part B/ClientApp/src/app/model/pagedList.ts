export interface IPagedList<T> {
    total: number;
    pageSize: number | null;
    items: T[];
}