import { IGlossaryView } from "./../model/glossary";
import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges, ViewChild } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { NgbModal, NgbModalRef } from "@ng-bootstrap/ng-bootstrap";
import { GlossaryService } from "../service/glossary.service";
import { ToastrService } from "ngx-toastr";

@Component({
  selector: "app-glossary-edit",
  templateUrl: "./glossary-edit.component.html"
})
export class GlossaryEditComponent implements OnChanges {
  @Output() reload: EventEmitter<boolean> = new EventEmitter();
  @Input() open: boolean;
  @Input() id: number;
  @ViewChild("content") modal: any;
  glossaryForm: FormGroup = this.initForm();
  closeResult = "";

  constructor(private gs: GlossaryService, private modalService: NgbModal, private toastr: ToastrService) { }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes.id && changes.id.currentValue != null && changes.id.currentValue !== -1) {
      this.id = changes.id.currentValue;
      this.gs.getGlossary(this.id).subscribe(x => {
        this.glossaryForm.controls.term.setValue(x.term);
        this.glossaryForm.controls.definition.setValue(x.definition);
        this.glossaryForm.controls.id.setValue(x.id);
        const modalRef: NgbModalRef = this.modalService.open(this.modal);
        modalRef.result.then((data) => {
          // on close
          this.reload.emit(true);
        },
        (error) => {
          // on error/dismiss
          this.reload.emit(true);
        });
      });
    }
  }

  add(content: any): void {
    this.glossaryForm = this.initForm();
    this.modalService.open(content, { ariaLabelledBy: "modal-basic-title" }).result.then(
      (result) => {
        if (result === "Save") {
          this.save();
        }
      },
      (reason) => {
        this.closeResult = `Dismissed ${reason}`;
      },
    );
  }

  initForm(): FormGroup {
    return new FormGroup({
      id: new FormControl(null),
      term: new FormControl(null, Validators.required),
      definition: new FormControl(null, Validators.required)
    });
  }

  save(): void {
    this.glossaryForm.markAsDirty();
    if (this.glossaryForm.invalid) {
      return;
    }
    const data: IGlossaryView = this.glossaryForm.getRawValue() as IGlossaryView;
    this.gs.upSert(data).subscribe(x => {
      this.toastr.success("Success");
      this.reload.emit(true);
      this.modalService.dismissAll();
    });
  }

}
