//
//  Lesson.swift
//  schedule.ios.application
//
//  Created by Иван Светлов on 11.06.2023.
//

import Foundation

struct Lesson: Identifiable, Codable, Hashable, Equatable {
    var id: Int
    var number: Int
    var subgroup: Int?
    var timetableId: Int
    var isChanged: Bool
    var time: Time?
    var discipline: Discipline?
    var teacherClassrooms: [TeacherClassroom]
}
