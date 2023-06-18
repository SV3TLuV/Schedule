//
//  TeacherClassroom.swift
//  schedule.ios.application
//
//  Created by Иван Светлов on 11.06.2023.
//

import Foundation

struct TeacherClassroom: Codable, Hashable, Equatable {
    var teacher: Teacher
    var classroom: Classroom?
}
