import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { IGlossaryView } from "../model/glossary";
import { IPagedList } from "../model/pagedList";

@Injectable({
  providedIn: "root"
})
export class GlossaryService {
  private appBaseUrl = "";
  private headers: HttpHeaders = new HttpHeaders({
    "Content-Type": "application/json"
  });
  constructor(private http: HttpClient, @Inject("BASE_URL") baseUrl: string) {
    this.appBaseUrl = baseUrl;
  }

  getPagedGlossaryList(filter: any): Observable<IPagedList<IGlossaryView>> {
    return this.http.get<IPagedList<IGlossaryView>>(this.appBaseUrl + "glossary/list", { headers: this.headers, params: filter });
  }

  upSert(data: IGlossaryView): Observable<number> {
    return this.http.post<any>(this.appBaseUrl + "glossary/upsert", data, { headers: this.headers });
  }

  getGlossary(id: number): Observable<IGlossaryView> {
    return this.http.get<IGlossaryView>(this.appBaseUrl + `glossary/${id}`, { headers: this.headers });
  }

  delete(id: number): Observable<number> {
    return this.http.delete<any>(this.appBaseUrl + `glossary/${id}`, { headers: this.headers });
  }

}
