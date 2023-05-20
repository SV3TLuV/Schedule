import {IClassroom} from "../features/models/IClassroom";
import {IDiscipline} from "../features/models/IDiscipline";
import {IDisciplineType} from "../features/models/IDisciplineType";
import {ISpeciality} from "../features/models/ISpeciality";
import {ITerm} from "../features/models/ITerm";
import {ICourse} from "../features/models/ICourse";
import {IGroup} from "../features/models/IGroup";

export const emptyClassroom: IClassroom = {
    id: 0,
    cabinet: '',
    types: [],
    isDeleted: false
}

export const emptyDisciplineType: IDisciplineType = {
    id: 0,
    name: ''
}

export const emptySpeciality: ISpeciality = {
    id: 0,
    code: '',
    name: '',
    maxTermId: 0,
    disciplines: [],
    isDeleted: false
}

export const emptyCourse: ICourse = {
    id: 0
}

export const emptyTerm: ITerm = {
    id: 0,
    courseTerm: 0,
    course: emptyCourse
}

export const emptyDiscipline: IDiscipline = {
    id: 0,
    name: '',
    code: '',
    totalHours: 0,
    type: emptyDisciplineType,
    term: emptyTerm,
    speciality: emptySpeciality,
    isDeleted: false
}

export const emptyGroup: IGroup = {
    id: 0,
    name: '',
    number: '',
    enrollmentYear: 0,
    term: emptyTerm,
    speciality: emptySpeciality,
    mergedGroups: [],
    isDeleted: false
}