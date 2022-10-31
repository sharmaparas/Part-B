import { Component, OnInit } from "@angular/core";
import { IGlossaryView } from "../model/glossary";
import { GlossaryService } from "../service/glossary.service";
import { ToastrService } from "ngx-toastr";
@Component({
  selector: "app-glossary-list",
  templateUrl: "./glossary-list.component.html"
})
export class GlossaryListComponent implements OnInit {
  page = 1;
  pageSize = 10;
  pageSizeList = [5, 10, 20];
  collectionSize = 10;
  model: IGlossaryView[] = [];
  glossaryId: number;
  openModal = false;

  constructor(private gs: GlossaryService, private toastr: ToastrService) {
  }
  ngOnInit(): void {
    this.init(false);
  }

  init(loadDefault: boolean): void {
    this.glossaryId = -1;
    if (loadDefault) {
      this.page = 1;
      this.pageSize = 10;
    }
    this.pageChange();
  }

  pageChange(): void {
    this.gs.getPagedGlossaryList({ page: this.page, itemsPerPage: this.pageSize }).subscribe(x => {
      this.model = x.items;
      this.collectionSize = x.total;
    });
  }

  editGlossary(id: number): void {
    this.glossaryId = id;
  }

  deleteGlossary(id: number): void {
    this.gs.delete(id).subscribe(x => {
      this.init(true);
      this.toastr.success("Deleted");
    });
  }
}